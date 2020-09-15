using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository.IEntityRepositories;
using WorkSharp.ViewModels.User;

namespace WorkSharp.Controllers.User
{
    public class TeamsController : Controller
    {
        private readonly ITeamRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<DbUser> _userManager;
        public TeamsController(ITeamRepository repository, IMapper mapper, UserManager<DbUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IActionResult Team(Guid teamId)
        {
            var userId = GetUserId();
            var dbTeam = _repository.GetByIdSecure(teamId, userId);
            var teamViewModel = _mapper.Map<TeamViewModel>(dbTeam);
            return View("~/Views/User/Teams/Team.cshtml", teamViewModel);
        }

        public IActionResult RemoveTeam(Guid teamId)
        {
            var userId = GetUserId();
            var taskBoardProjectId = _repository.GetByIdSecure(teamId, userId).ProjectId;
            _repository.DeleteSecure(teamId, userId);
            _repository.Save();
            return RedirectToAction("Project", "Projects", new { projectId = taskBoardProjectId });
        }
        private Guid GetUserId()
        {
            return _userManager.GetUserAsync(HttpContext.User).Result.Id;
        }
    }
}
