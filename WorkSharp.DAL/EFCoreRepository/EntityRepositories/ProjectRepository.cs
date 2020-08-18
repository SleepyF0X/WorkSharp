using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository.IEntityRepositories;

namespace WorkSharp.DAL.EFCoreRepository.EntityRepositories
{
    public class ProjectRepository : IProjectRepository
    {
        private WorkSharpDbContext _context;
        private DbSet<DbProject> _dbSet;
        public ProjectRepository(WorkSharpDbContext context)
        {
            _context = context;
            _dbSet = _context.Projects;
        }

        public IReadOnlyCollection<DbProject> GetAll()
        {
            return _dbSet.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<DbProject> GetUserProjects(Guid userId)
        {
            var userProjects = _dbSet.AsNoTracking().Where(proj => proj.CreatorId.Equals(userId)).ToList().AsReadOnly();
            return userProjects;
        }

        public DbProject GetByIdSecure(Guid id, Guid userId)
        {
            var userProjects = GetUserProjects(userId);
            var project = userProjects.FirstOrDefault(project => project.Id.Equals(id));
            return project;
        }

        public bool DeleteSecure(Guid id, Guid userId)
        {
            var userProjects = GetUserProjects(userId);
            var project = userProjects.FirstOrDefault(project => project.Id.Equals(id));
            if (project != null)
            {
                _context.Remove(project);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Create(DbProject dbProject)
        {
            _dbSet.Add(dbProject);
        }

        public bool UpdateSecure(DbProject dbProject, Guid userId)
        {
            var userProjects = GetUserProjects(userId);
            if (userProjects.Any(project => project.Id.Equals(dbProject.Id)))
            {
                _dbSet.Update(dbProject);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
