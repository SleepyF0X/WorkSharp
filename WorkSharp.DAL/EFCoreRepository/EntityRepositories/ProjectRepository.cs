using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.DbModels.Relations;
using WorkSharp.DAL.EFCoreRepository.IEntityRepositories;

namespace WorkSharp.DAL.EFCoreRepository.EntityRepositories
{
    public class ProjectRepository : IProjectRepository
    {
        private WorkSharpDbContext _context;
        private DbSet<DbProject> _dbSet;
        private ITeamRepository _teamRepository;
        public ProjectRepository(WorkSharpDbContext context, ITeamRepository teamRepository)
        {
            _context = context;
            _dbSet = _context.Projects;
            _teamRepository = teamRepository;
        }

        public IReadOnlyCollection<DbProject> GetAll()
        {
            return _dbSet.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<DbProject> GetUserProjects(Guid userId)
        {
            var projects = _context.Users.AsNoTracking().SelectMany(user => user.TeamMembers.Select(tm => tm.Team.Project)).ToList().AsReadOnly();
            return projects;
        }

        public DbProject GetByIdSecure(Guid id, Guid userId)
        {
            var userProjects = GetUserProjects(userId);
            var project = userProjects.FirstOrDefault(project => project.Id.Equals(id));
            project.TaskBoards = _context.TaskBoards.Where(tb => tb.ProjectId.Equals(project.Id)).ToList();
            project.Teams = _context.Teams.Where(t=>t.ProjectId.Equals(project.Id)).ToList();
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

        public void Create(DbProject dbProject, Guid memberId)
        {
            _dbSet.Attach(dbProject);
            var team = new DbTeam
            {
                Name = "Name",
                ProjectId = dbProject.Id,
                TeamMembers = new List<DbTeamMembers>(),
                Info = "null"
            };
            team.TeamMembers.Add(new DbTeamMembers
            {
                TeamId = team.Id,
                MemberId = memberId
            });
            _context.Teams.Attach(team);
            _context.SaveChanges();
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
