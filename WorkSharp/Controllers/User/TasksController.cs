using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ISolutionRepository _solutionRepository;
        public TasksController(ISolutionRepository solutionRepository, ITaskRepository repository, IMapper mapper, UserManager<DbUser> userManager, IProjectRepository projectRepository, ITaskBoardRepository taskBoardRepository, IWebHostEnvironment appEnvironment)
        {
            _taskBoardRepository = taskBoardRepository;
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _projectRepository = projectRepository;
            _appEnvironment = appEnvironment;
            _solutionRepository = solutionRepository;
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
            else
            {
                TempData["ErrorMessage"] = "You are not member of a team :(";
            }
            return RedirectToAction("TaskBoard", "TaskBoards", new { taskBoardId });
        }

        public async Task<IActionResult> AddSolution(IFormFile solutionFile, Guid taskId, Guid taskBoardId)
        {
            var path = "/solutions/" + solutionFile.FileName;
            await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await solutionFile.CopyToAsync(fileStream);
            }
            var solution = new DbSolution() { Name = solutionFile.FileName, Path = path };
            _repository.AddSolution(solution, taskId);
            return RedirectToAction("TaskBoard", "TaskBoards", new { taskBoardId });
        }

        public HttpResponseMessage DownloadSolution(Guid solutionId)
        {
            var solution = _solutionRepository.GetSolution(solutionId);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
                var filePath = _appEnvironment.WebRootPath + solution.Path;
                var bytes = System.IO.File.ReadAllBytes(filePath);

                result.Content = new ByteArrayContent(bytes);

                var mediaType = "application/octet-stream";
                result.Content.Headers.ContentType = new  System.Net.Http.Headers.MediaTypeHeaderValue(mediaType);
                return result;
        }
        private Guid GetUserId()
        {
            return Guid.Parse((ReadOnlySpan<char>) HttpContext.User.Claims.SingleOrDefault(c=>c.Type.Equals("Id"))?.Value);
        }
    }
}
