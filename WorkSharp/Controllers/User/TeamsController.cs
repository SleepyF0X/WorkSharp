using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository.IEntityRepositories;
using WorkSharp.ViewModels.User;
using WorkSharp.ViewModels.User.Multiply;

namespace WorkSharp.Controllers.User
{
    public class TeamsController : Controller
    {
        private readonly ITeamRepository _repository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<DbUser> _userManager;
        public TeamsController(ITeamRepository repository, IMapper mapper, UserManager<DbUser> userManager, IProjectRepository projectRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _projectRepository = projectRepository;
        }

        public IActionResult Team(Guid teamId, Guid projId)
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }
            ViewBag.AdminAreas = _projectRepository.IsAdmin(projId, GetUserId());
            var userId = GetUserId();
            var dbTeam = _repository.GetByIdSecure(teamId, userId);
            if (dbTeam == null)
            {
                TempData["ErrorMessage"] = "Not Access!";
                return RedirectToAction("Project", "Projects", new { projectId = projId });
            }
            else
            {
                var teamViewModel = _mapper.Map<TeamViewModel>(dbTeam);
                var users = _mapper.Map<List<UserViewModel>>(_userManager.Users);
                ViewData["Team"] = teamViewModel;
                ViewData["Users"] = new SelectList(users, "Id", "UserName");;
                return View("~/Views/User/Teams/Team.cshtml");
            }
        }

        public IActionResult RemoveTeam(Guid teamId)
        {
            var userId = GetUserId();
            var taskBoardProjectId = _repository.GetByIdSecure(teamId, userId).ProjectId;
            _repository.DeleteSecure(teamId, userId);
            _repository.Save();
            return RedirectToAction("Project", "Projects", new { projectId = taskBoardProjectId });
        }
        public IActionResult CreateTeam(TeamTaskBoardViewModel teamTaskBoardViewModel)
        {
            var userId = GetUserId();
            var teamViewModel = teamTaskBoardViewModel.TeamViewModel;
            var dbTeam = _mapper.Map<DbTeam>(teamViewModel);
            if (_repository.CreateTeam(dbTeam, teamTaskBoardViewModel.TeamViewModel.ProjectId, userId))
            {
                _repository.Save();
                return RedirectToAction("Project", "Projects", new{projectId = teamViewModel.ProjectId});
            }
            TempData["ErrorMessage"] = "Not Access!";
            return RedirectToAction("Project", "Projects", new { projectId = teamTaskBoardViewModel.TeamViewModel.ProjectId });
        }
        public IActionResult AddMember(Guid teamId, TeamViewModel teamViewModel, Guid projId)
        {
            //var userId = userViewModel.Id;
            var userId = teamViewModel.MemberId;
            if(_repository.AddMember(teamId, userId))
            {
                _repository.Save();
                return RedirectToAction("Team", "Teams", new{teamId, projId});
            }

            TempData["ErrorMessage"] = "User Exist";
            return RedirectToAction("Team", "Teams", new { teamId, projId });

        }
        public IActionResult RemoveMember(Guid teamId, Guid userId, Guid projId)
        {
            //var userId = userViewModel.Id;
            //var userId = teamViewModel.MemberId;
            _repository.RemoveMember(teamId, userId);
            _repository.Save();
            return RedirectToAction("Team", "Teams", new{teamId, projId});
        }
        private Guid GetUserId()
        {
            return _userManager.GetUserAsync(HttpContext.User).Result.Id;
        }
    }
}
