using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository.IEntityRepositories;

namespace WorkSharp.DAL.EFCoreRepository.EntityRepositories
{
    public class TaskRepository : ITaskRepository
    {
        private WorkSharpDbContext _context;
        private DbSet<DbTask> _dbSet;

        public TaskRepository(WorkSharpDbContext context)
        {
            _context = context;
            _dbSet = _context.Tasks;
        }

        public IReadOnlyCollection<DbTask> GetTaskBoardTasks(Guid taskBoardId)
        {
            return _dbSet.Include(t => t.Executor).Include(t => t.Solution).Where(t => t.TaskBoardId.Equals(taskBoardId)).ToList().AsReadOnly();
        }

        public void CreateTask(DbTask task)
        {
            if (task.ExecutorId.Equals(Guid.Empty))
            {
                task.ExecutorId = null;
            }
            if (task.SolutionId.Equals(Guid.Empty))
            {
                task.SolutionId = null;
            }
            _dbSet.Add(task);
            _context.SaveChanges();
        }

        public void DeleteTask(Guid taskId)
        {
            _dbSet.Remove(_dbSet.Find(taskId));
            _context.SaveChanges();
        }

        public void AddExecutor(Guid taskId, Guid userId)
        {
            var task = _dbSet.Find(taskId);
            task.ExecutorId = userId;
            _dbSet.Update(task);
            _context.SaveChanges();
        }

        public void AddSolution(DbSolution solution, Guid taskId)
        {
            var task = _dbSet.Find(taskId);
            task.Solution = solution;
            _dbSet.Update(task);
            _context.SaveChanges();
        }
    }
}