using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository;
using WorkSharp.DAL.EFCoreRepository.IEntityRepositories;
using WorkSharp.ViewModels;

namespace WorkSharp.Controllers.User
{
    [Authorize]
    public class UserProjectsController : Controller
    {
        private readonly IProjectRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<DbUser> _userManager;
        public UserProjectsController(IProjectRepository repository, IMapper mapper, UserManager<DbUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public IActionResult Projects()
        {
            var userId = GetUserId();
            var dbProjects = _repository.GetUserProjects(userId);
            var projectViewModels = _mapper.Map<IEnumerable<ProjectViewModel>>(dbProjects);
            ViewData["Projects"] = projectViewModels;
            return View("~/Views/User/Projects/Projects.cshtml");
        }

        public IActionResult CreateProject(ProjectViewModel projectViewModel)
        {
            projectViewModel.CreatorId = GetUserId();
            var dbProject = _mapper.Map<DbProject>(projectViewModel);
            _repository.Create(dbProject);
            _repository.Save();
            return RedirectToAction("Projects");
        }

        public IActionResult RemoveProject(Guid projectId)
        {
            var userId = GetUserId();
            var result = _repository.DeleteSecure(projectId, userId);
            if (result)
            {
                _repository.Save();
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
            var dbProject = _repository.GetByIdSecure(projectId, userId);
            if (dbProject != null)
            {
                var projectViewModel = _mapper.Map<ProjectViewModel>(dbProject);
                return View("~/Views/User/Projects/Project.cshtml", projectViewModel);
            }
            else
            {
                return View("~/Views/AccessDenied.cshtml");
            }
        }

        public IActionResult GetEditProject(Guid projectId)
        {
            var userId = GetUserId();
            var dbProject = _repository.GetByIdSecure(projectId, userId);
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
            model.CreatorId = userId;
            var dbProject = _mapper.Map<DbProject>(model);
            var result = _repository.UpdateSecure(dbProject, userId);
            if (result)
            {
                _repository.Save();
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
