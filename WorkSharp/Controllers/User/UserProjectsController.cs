using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository;
using WorkSharp.ViewModels;

namespace WorkSharp.Controllers.User
{
    [Authorize]
    public class UserProjectsController : Controller
    {
        private readonly IGenericRepository<DbProject> _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<DbUser> _userManager;
        public UserProjectsController(IGenericRepository<DbProject> repository, IMapper mapper, UserManager<DbUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public IActionResult Projects()
        {
            var userId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            var dbProjects = _repository.GetAll().Where(project => project.CreatorId.Equals(userId));
            var projectViewModels = _mapper.Map<IEnumerable<ProjectViewModel>>(dbProjects);
            ViewData["Projects"] = projectViewModels;
            return View("~/Views/User/Projects/Projects.cshtml");
        }

        public IActionResult CreateProject(ProjectViewModel projectViewModel)
        {
            projectViewModel.CreatorId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            var dbProject = _mapper.Map<DbProject>(projectViewModel);
            _repository.Create(dbProject);
            _repository.Save();
            return RedirectToAction("Projects");
        }

        public IActionResult RemoveProject(Guid id)
        {
            if (CheckAccess(id))
            {
                _repository.Delete(id);
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
            if (CheckAccess(projectId))
            {
                var dbProject = _repository.GetById(projectId);
                var projectViewModel = _mapper.Map<ProjectViewModel>(dbProject);
                return View("~/Views/User/Projects/Project.cshtml", projectViewModel);
            }
            else
            {
                return View("~/Views/AccessDenied.cshtml");
            }
        }

        public IActionResult GetEditProject(Guid id)
        {
            if (CheckAccess(id))
            {
                var dbProject = _repository.GetById(id);
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
            if (CheckAccess(model.Id))
            {
                var dbProject = _mapper.Map<DbProject>(model);
                _repository.Update(dbProject);
                _repository.Save();
                return RedirectToAction("Project", new { id = model.Id });
            }
            else
            {
                return View("~/Views/AccessDenied.cshtml");
            }
        }

        private bool CheckAccess(Guid projectId)
        {
            var userId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            var projectCreatorId = _repository.GetById(projectId).CreatorId;
            if (projectCreatorId.Equals(userId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
