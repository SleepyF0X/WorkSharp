using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository;
using WorkSharp.ViewModels;

namespace WorkSharp.Controllers
{
    [Authorize]
    public class UserProjectsController : Controller
    {
        private readonly IGenericRepository<DbProject> _repository;
        private IMapper _mapper;
        private UserManager<DbUser> _userManager;
        public UserProjectsController(IGenericRepository<DbProject> repository, IMapper mapper, UserManager<DbUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public IActionResult Projects()
        {
            var userId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            ViewData["Projects"] = _mapper.Map<IEnumerable<ProjectViewModel>>(_repository.GetAll().Where(project => project.CreatorId.Equals(userId)));
            return View("~/Views/User/Projects/Projects.cshtml");
        }

        public IActionResult CreateProject(ProjectViewModel projectViewModel)
        {
            projectViewModel.CreatorId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            _repository.Create(_mapper.Map<DbProject>(projectViewModel));
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
                var model = _mapper.Map<ProjectViewModel>(_repository.GetById(projectId));
                return View("~/Views/User/Projects/Project.cshtml", model);
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
                var model = _mapper.Map<ProjectViewModel>(_repository.GetById(id));
                return View("~/Views/User/Projects/EditProject.cshtml", model);
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
                _repository.Update(_mapper.Map<DbProject>(model));
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
