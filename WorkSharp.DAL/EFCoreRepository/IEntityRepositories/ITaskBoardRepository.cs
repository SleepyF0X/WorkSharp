using System;
using System.Collections.Generic;
using WorkSharp.DAL.DbModels;

namespace WorkSharp.DAL.EFCoreRepository.IEntityRepositories
{
    public interface ITaskBoardRepository
    {
        public IReadOnlyCollection<DbTaskBoard> GetAll();

        public IReadOnlyCollection<DbTaskBoard> GetUserTaskBoards(Guid userId);

        public DbTaskBoard GetByIdSecure(Guid id, Guid userId);

        public bool DeleteSecure(Guid id, Guid userId);

        public void Create(DbTaskBoard dbTaskBoard);

        public bool UpdateSecure(DbTaskBoard dbTaskBoard, Guid userId);

        public void Save();
    }
}