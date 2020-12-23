using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSharp.DAL.DbModels;

namespace WorkSharp.DAL.EFCoreRepository.IEntityRepositories
{
    public interface ITaskRepository
    {
        public IReadOnlyCollection<DbTask> GetTaskBoardTasks(Guid taskBoardId);
        public void CreateTask(DbTask task);
        public void DeleteTask(Guid taskId);
        public void AddExecutor(Guid taskId, Guid userId);
    }
}
