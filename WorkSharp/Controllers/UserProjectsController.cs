using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository;
using WorkSharp.ViewModels;

namespace WorkSharp.Controllers
{
    public class UserProjectsController : Controller
    {
        private readonly IGenericRepository<DbProject> _repository;
        private IMapper _mapper;
        public UserProjectsController(IGenericRepository<DbProject> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public IActionResult Projects()
        {
            ViewData["Projects"] = _mapper.Map<IEnumerable<ProjectViewModel>>(_repository.GetAll());
            return View("~/Views/User/Projects.cshtml");
        }

        public IActionResult CreateProject(ProjectViewModel projectViewModel)
        {
            _repository.Create(_mapper.Map<DbProject>(projectViewModel));
            _repository.Save();
            return RedirectToAction("Projects");
        }

        public IActionResult RemoveProject(Guid id)
        {
            _repository.Delete(id);
            _repository.Save();
            return RedirectToAction("Projects");
        }

        public IActionResult Project(Guid id)
        {
            var model = _mapper.Map<ProjectViewModel>(_repository.GetById(id));
            return View("~/Views/User/Project.cshtml", model);
        }
    }
}
