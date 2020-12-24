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
            var projects = _context.Projects
                .AsNoTracking()
                .Include(p => p.Admins)
                .Include(p=>p.Creator)
                .Include(p => p.Teams)
                .ThenInclude(t=>t.Members)
                .Where(p => p.Admins.Select(a => a.Id).Contains(userId) ||
                            p.Teams.SelectMany(t => t.Members).Any(m=>m.Id.Equals(userId))).ToList().AsReadOnly();
            return projects;
        }

        public DbProject GetByIdSecure(Guid id, Guid userId)
        {
            var userProjects = GetUserProjects(userId);
            var project = userProjects.FirstOrDefault(project => project.Id.Equals(id));
            if (project != null)
            {
                project.TaskBoards = _context.TaskBoards
                    .Include(tb=>tb.Team)
                    .ThenInclude(t=>t.Members)
                    .Where(tb => tb.ProjectId.Equals(project.Id))
                    .ToList();
                return project;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteSecure(Guid projectId, Guid userId)
        {
            var isAdmin = IsAdmin(projectId, userId);
            if (isAdmin)
            {
                var project = _context.Projects.Find(projectId);
                _context.Remove(project);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Create(DbProject dbProject, Guid creatorId)
        {
            dbProject.Admins.Add(_context.Users.Find(creatorId));
            dbProject.CreatorId = creatorId;
            _dbSet.Add(dbProject);
        }

        public bool UpdateSecure(DbProject dbProject, Guid userId)
        {
            dbProject.CreatorId = userId;
            var isAdmin = IsAdmin(dbProject.Id, userId);
            if (isAdmin)
            {
                _dbSet.Update(dbProject);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAdmin(Guid projectId, Guid userId)
        {
            if (_context.Projects.AsNoTracking().Include(p => p.Admins).Include(p=>p.Creator).FirstOrDefault(p => p.Id.Equals(projectId)).Admins
                .Any(a => a.Id.Equals(userId)))
            {
                return true;
            }

            return false;
        }

        public bool IsCreator(Guid projectId, Guid userId)
        {
            if (_context.Projects.AsNoTracking().FirstOrDefault(p=>p.Id.Equals(projectId)).CreatorId.Equals(userId))
            {
                return true;
            }

            return false;
        }
        public bool AddAdmin(Guid projectId, Guid userId)
        {
            var dbProject = _dbSet.Include(p=>p.Admins).FirstOrDefault(p => p.Id.Equals(projectId));
            if (dbProject.Admins.Select(m => m.Id).Contains(userId))
            {
                return false;
            }
            dbProject.Admins.Add(_context.Users.Find(userId));
            _context.SaveChanges();
            return true;
        }

        public bool RemoveAdmin(Guid projectId, Guid userId)
        {
            var dbProject = _dbSet.Include(p=>p.Admins).FirstOrDefault(p => p.Id.Equals(projectId));
            if (!dbProject.Admins.Select(m => m.Id).Contains(userId))
            {
                return false;
            }
            dbProject.Admins.Remove(_context.Users.Find(userId));
            _context.SaveChanges();
            return true;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
