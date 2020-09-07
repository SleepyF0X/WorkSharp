using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.DbModels.Relations;
using WorkSharp.DAL.EFCoreRepository;
using WorkSharp.DAL.EFCoreRepository.IEntityRepositories;
using WorkSharp.ViewModels;
using WorkSharp.ViewModels.User;

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
            var userId = GetUserId();
            var dbProject = _projectRepository.GetByIdSecure(projectId, userId);
            if (dbProject != null)
            {
                var projectViewModel = _mapper.Map<ProjectViewModel>(dbProject);
                ViewData["Project"] = projectViewModel;
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

        private Guid GetUserId()
        { 
            return _userManager.GetUserAsync(HttpContext.User).Result.Id;
        }
    }
}
