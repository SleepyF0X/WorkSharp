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
using WorkSharp.ViewModels.User.Multiply;

namespace WorkSharp.Controllers.User
{
    public class TaskBoardsController : Controller
    {
        private readonly ITaskBoardRepository _repository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<DbUser> _userManager;
        public TaskBoardsController(ITaskBoardRepository repository, IMapper mapper, UserManager<DbUser> userManager, IProjectRepository projectRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _projectRepository = projectRepository;
        }
        public IActionResult TaskBoard(Guid taskBoardId)
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }
            var userId = GetUserId();
            var dbTaskBoard = _repository.GetByIdSecure(taskBoardId, userId);
            var taskBoardViewModel = _mapper.Map<TaskBoardViewModel>(dbTaskBoard);
            ViewBag.AdminAreas = _projectRepository.IsAdmin(taskBoardViewModel.ProjectId, GetUserId());
            ViewData["TaskBoard"] = taskBoardViewModel;
            return View("~/Views/User/TaskBoards/TaskBoard.cshtml");
        }

        public IActionResult RemoveTaskBoard(Guid taskBoardId, string page)
        {
            if (page == "project")
            {
                var userId = GetUserId();
                var taskBoardProjectId = _repository.GetByIdSecure(taskBoardId, userId).ProjectId;
                _repository.DeleteSecure(taskBoardId, userId);
                _repository.Save();
                return RedirectToAction("Project", "Projects", new {projectId = taskBoardProjectId});
            }
            else
            {
                var userId = GetUserId();
                var taskBoard = _repository.GetByIdSecure(taskBoardId, userId);
                _repository.DeleteSecure(taskBoardId, userId);
                _repository.Save();
                return RedirectToAction("Team", "Teams", new {taskBoard.TeamId, projId = taskBoard.ProjectId});
            }
        }

        public IActionResult CreateTaskBoard(TeamTaskBoardViewModel teamTaskBoardViewModel, string page)
        {
            if (page == "project")
            {
                var taskBoardViewModel = teamTaskBoardViewModel.TaskBoardViewModel;
                var dbTaskBoard = _mapper.Map<DbTaskBoard>(taskBoardViewModel);
                _repository.Create(dbTaskBoard);
                _repository.Save();
                return RedirectToAction("Project", "Projects", new{projectId = taskBoardViewModel.ProjectId});
            }

            else
            {
                var taskBoardViewModel = teamTaskBoardViewModel.TaskBoardViewModel;
                var dbTaskBoard = _mapper.Map<DbTaskBoard>(taskBoardViewModel);
                _repository.Create(dbTaskBoard);
                _repository.Save();
                return RedirectToAction("Team", "Teams", new{taskBoardViewModel.TeamId, projId = taskBoardViewModel.ProjectId});
            }
            
        }
        //public IActionResult CreateTaskBoard(TaskBoardViewModel taskBoardViewModel)
        //{
        //    var dbTaskBoard = _mapper.Map<DbTaskBoard>(taskBoardViewModel);
        //    _repository.Create(dbTaskBoard);
        //    _repository.Save();
        //    return RedirectToAction("Team", "Teams", new{taskBoardViewModel.TeamId, taskBoardViewModel.ProjectId});
        //}
        private Guid GetUserId()
        {
            return _userManager.GetUserAsync(HttpContext.User).Result.Id;
        }
    }
}
