using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository;
using WorkSharp.ViewModels;

namespace WorkSharp.Controllers
{
    public class MainController : Controller
    {
        private readonly IGenericRepository<DbProject> _repository;
        private IMapper _mapper;
        public MainController(IGenericRepository<DbProject> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            IReadOnlyCollection<DbProject> projects = _repository.GetAll();
            IReadOnlyCollection<ProjectViewModel> projectViewModels = _mapper.Map<IReadOnlyCollection<ProjectViewModel>>(projects);
            return View(projectViewModels);
        }
    }
}
