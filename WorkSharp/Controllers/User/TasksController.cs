using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository.IEntityRepositories;
using WorkSharp.ViewModels.User;

namespace WorkSharp.Controllers.User
{
    public class TasksController : Controller
    {
        private readonly ITaskRepository _repository;
        private readonly ITaskBoardRepository _taskBoardRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<DbUser> _userManager;
        public TasksController(ITaskRepository repository, IMapper mapper, UserManager<DbUser> userManager, IProjectRepository projectRepository, ITaskBoardRepository taskBoardRepository)
        {
            _taskBoardRepository = taskBoardRepository;
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _projectRepository = projectRepository;
        }

        public IActionResult CreateTask(TaskViewModel taskViewModel, Guid projectId)
        {
            if (_projectRepository.IsAdmin(projectId, GetUserId()))
            {
                var dbTask = _mapper.Map<DbTask>(taskViewModel);
                _repository.CreateTask(dbTask);
            }
            return RedirectToAction("TaskBoard", "TaskBoards", new { taskViewModel.TaskBoardId });
        }
        public IActionResult RemoveTask(Guid taskId, Guid projectId, Guid taskBoardId)
        {
            if (_projectRepository.IsAdmin(projectId, GetUserId()))
            {
                _repository.DeleteTask(taskId);
            }
            return RedirectToAction("TaskBoard", "TaskBoards", new { taskBoardId });
        }

        public IActionResult AddExecutor(Guid taskId, Guid taskBoardId)
        {
            var taskBoard = _taskBoardRepository.GetByIdSecure(taskBoardId, GetUserId());
            var ids = taskBoard.Team.Members.Select(m => m.Id).ToList();
            var userId = GetUserId();
            if (ids.Contains(userId))
            {
                _repository.AddExecutor(taskId, GetUserId());
            }
            return RedirectToAction("TaskBoard", "TaskBoards", new { taskBoardId });
        }
        private Guid GetUserId()
        {
            return Guid.Parse((ReadOnlySpan<char>) HttpContext.User.Claims.SingleOrDefault(c=>c.Type.Equals("Id"))?.Value);
        }
    }
}
