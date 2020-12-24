using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository.IEntityRepositories;
using WorkSharp.ViewModels.User;
using WorkSharp.ViewModels.User.Multiply;

namespace WorkSharp.Controllers.User
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<DbUser> _userManager;

        public ProjectsController(IProjectRepository projectRepository, IMapper mapper, UserManager<DbUser> userManager)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IActionResult Projects()
        {
            var userId = GetUserId();
            var dbProjects = _projectRepository.GetUserProjects(userId);
            var projectViewModels = _mapper.Map<IEnumerable<ProjectViewModel>>(dbProjects);
            ViewData["Projects"] = projectViewModels;
            return View("~/Views/User/Projects/Projects.cshtml");
        }

        public IActionResult CreateProject(ProjectViewModel projectViewModel)
        {
            var dbProject = _mapper.Map<DbProject>(projectViewModel);
            _projectRepository.Create(dbProject, GetUserId());
            _projectRepository.Save();
            return RedirectToAction("Projects");
        }

        public IActionResult RemoveProject(Guid projectId)
        {
            var userId = GetUserId();
            var result = _projectRepository.DeleteSecure(projectId, userId);
            if (result)
            {
                _projectRepository.Save();
                return RedirectToAction("Projects");
            }
            else
            {
                return View("~/Views/AccessDenied.cshtml");
            }
        }

        public IActionResult Project(Guid projectId)
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }

            ViewBag.AdminAreas = _projectRepository.IsAdmin(projectId, GetUserId());
            ViewBag.CreatorAreas = _projectRepository.IsCreator(projectId, GetUserId());
            var userId = GetUserId();
            var dbProject = _projectRepository.GetByIdSecure(projectId, userId);
            if (dbProject != null)
            {
                var projectViewModel = _mapper.Map<ProjectViewModel>(dbProject);
                ViewData["Project"] = projectViewModel;
                ViewData["Members"] = new SelectList(projectViewModel.Members, "Id", "UserName");
                ViewData["Teams"] = new SelectList(projectViewModel.TeamViewModels, "Id", "Name");
                ViewData["Admins"] = new SelectList(projectViewModel.Admins, "Id", "UserName");
                return View("~/Views/User/Projects/Project.cshtml");
            }
            else
            {
                return View("~/Views/AccessDenied.cshtml");
            }
        }

        public IActionResult GetEditProject(Guid projectId)
        {
            var userId = GetUserId();
            var dbProject = _projectRepository.GetByIdSecure(projectId, userId);
            if (dbProject != null)
            {
                var projectViewModel = _mapper.Map<ProjectViewModel>(dbProject);
                return View("~/Views/User/Projects/EditProject.cshtml", projectViewModel);
            }
            else
            {
                return View("~/Views/AccessDenied.cshtml");
            }
        }

        public IActionResult EditProject(ProjectViewModel model)
        {
            var userId = GetUserId();
            var dbProject = _mapper.Map<DbProject>(model);
            var result = _projectRepository.UpdateSecure(dbProject, userId);
            if (result)
            {
                _projectRepository.Save();
                return RedirectToAction("Project", new { projectId = model.Id });
            }
            else
            {
                return View("~/Views/AccessDenied.cshtml");
            }
        }

        public async Task<IActionResult> AddAdmin(TeamTaskBoardViewModel teamTaskBoardViewModel, Guid projectId)
        {
            var userId = teamTaskBoardViewModel.NewAdminId;
            if (_projectRepository.AddAdmin(projectId, userId))
            {
                _projectRepository.Save();
                return RedirectToAction("Project", "Projects", new { projectId });
            }

            TempData["ErrorMessage"] = "Admin Exist";
            return RedirectToAction("Project", "Projects", new { projectId });
        }

        public async Task<IActionResult> RemoveAdmin(TeamTaskBoardViewModel teamTaskBoardViewModel, Guid projectId)
        {
            if (_projectRepository.GetByIdSecure(projectId, GetUserId()).CreatorId
                .Equals(teamTaskBoardViewModel.RemoveAdminId))
            {
                TempData["ErrorMessage"] = "You are Creator :)";
                return RedirectToAction("Project", "Projects", new { projectId });
            }
            var userId = teamTaskBoardViewModel.RemoveAdminId;
            if (_projectRepository.RemoveAdmin(projectId, userId))
            {
                _projectRepository.Save();
                return RedirectToAction("Project", "Projects", new { projectId });
            }

            TempData["ErrorMessage"] = "Admin NOT Exist";
            return RedirectToAction("Project", "Projects", new { projectId });
        }

        private Guid GetUserId()
        {
            return Guid.Parse((ReadOnlySpan<char>)HttpContext.User.Claims.SingleOrDefault(c => c.Type.Equals("Id"))?.Value);
        }
    }
}