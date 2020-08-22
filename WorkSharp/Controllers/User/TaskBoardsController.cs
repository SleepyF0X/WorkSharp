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
    public class TaskBoardsController : Controller
    {
        private readonly ITaskBoardRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<DbUser> _userManager;
        public TaskBoardsController(ITaskBoardRepository repository, IMapper mapper, UserManager<DbUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public IActionResult TaskBoard(Guid taskBoardId)
        {
            var userId = GetUserId();
            var dbTaskBoard = _repository.GetByIdSecure(taskBoardId, userId);
            var taskBoardViewModel = _mapper.Map<TaskBoardViewModel>(dbTaskBoard);
            return View("~/Views/User/TaskBoards/TaskBoard.cshtml", taskBoardViewModel);
        }

        public IActionResult RemoveTaskBoard(Guid taskBoardId)
        {
            var userId = GetUserId();
            var taskBoardProjectId = _repository.GetByIdSecure(taskBoardId, userId).ProjectId;
            _repository.DeleteSecure(taskBoardId, userId);
            _repository.Save();
            return RedirectToAction("Project", "Projects", new { projectId = taskBoardProjectId });
        }

        public IActionResult CreateTaskBoard(TaskBoardViewModel taskBoardViewModel)
        {
            var dbTaskBoard = _mapper.Map<DbTaskBoard>(taskBoardViewModel);
            _repository.Create(dbTaskBoard);
            _repository.Save();
            return RedirectToAction("Project", "Projects", new{projectId = taskBoardViewModel.ProjectId});
        }
        private Guid GetUserId()
        {
            return _userManager.GetUserAsync(HttpContext.User).Result.Id;
        }
    }
}
