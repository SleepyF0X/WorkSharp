using System;
using System.Collections.Generic;
using WorkSharp.DAL.DbModels;

namespace WorkSharp.DAL.EFCoreRepository.IEntityRepositories
{
    public interface IProjectRepository
    {
        public IReadOnlyCollection<DbProject> GetAll();

        public IReadOnlyCollection<DbProject> GetUserProjects(Guid userId);

        public DbProject GetByIdSecure(Guid id, Guid userId);

        public bool DeleteSecure(Guid id, Guid userId);

        public void Create(DbProject dbProject, Guid memberId);

        public bool UpdateSecure(DbProject dbProject, Guid userId);

        public bool IsAdmin(Guid projectId, Guid userId);

        public bool IsCreator(Guid projectId, Guid userId);

        public bool AddAdmin(Guid projectId, Guid userId);

        public bool RemoveAdmin(Guid projectId, Guid userId);

        public void Save();
    }
}