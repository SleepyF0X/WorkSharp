using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository.IEntityRepositories;

namespace WorkSharp.DAL.EFCoreRepository.EntityRepositories
{
    public class TeamRepository : ITeamRepository
    {
        private IProjectRepository _projectRepository;
        private WorkSharpDbContext _context;
        private DbSet<DbTeam> _dbSet;

        public TeamRepository(WorkSharpDbContext context, IProjectRepository projectRepository)
        {
            _context = context;
            _dbSet = _context.Teams;
            _projectRepository = projectRepository;
        }

        public bool CreateTeam(DbTeam dbTeam, Guid projectId, Guid userId)
        {
            if (_projectRepository.IsAdmin(projectId, userId))
            {
                dbTeam.CreatorId = userId;
                _dbSet.Add(dbTeam);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddMember(Guid teamId, Guid memberId)
        {
            var dbTeam = _dbSet.Include(t => t.Members).FirstOrDefault(t => t.Id.Equals(teamId));
            if (dbTeam.Members.Select(m => m.Id).Contains(memberId))
            {
                return false;
            }
            dbTeam.Members.Add(_context.Users.Find(memberId));
            _context.SaveChanges();
            return true;
        }

        public void RemoveMember(Guid teamId, Guid memberId)
        {
            var dbTeam = _dbSet.Include(t => t.Members).FirstOrDefault(t => t.Id.Equals(teamId));
            dbTeam.Members.Remove(_context.Users.Find(memberId));
            _context.SaveChanges();
        }

        public IReadOnlyCollection<DbTeam> GetUserTeams(Guid userId)
        {
            return _dbSet.Where(t => t.Members.Any(s => s.Id.Equals(userId)))
                .Include(team => team.Members)
                .ThenInclude(m => m.Teams)
                .Include(t => t.TaskBoards)
                .ToList()
                .AsReadOnly();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public DbTeam GetByIdSecure(Guid teamId, Guid userId)
        {
            var dbTeam = _context.Teams
                .Include(t => t.Members)
                .ThenInclude(m => m.Teams)
                .Include(t => t.TaskBoards)
                .FirstOrDefault(t => t.Id.Equals(teamId));
            var projectId = dbTeam.ProjectId;
            if (_projectRepository.IsAdmin(projectId, userId))
            {
                return dbTeam;
            }
            var userTeams = GetUserTeams(userId);
            dbTeam = userTeams.FirstOrDefault(team => team.Id.Equals(teamId));
            return dbTeam;
        }

        public bool DeleteSecure(Guid teamId, Guid userId)
        {
            if (_projectRepository.IsAdmin(_dbSet.Find(teamId).ProjectId, userId))
            {
                var dbTeam = _dbSet.Find(teamId);
                _dbSet.Remove(dbTeam);
                return true;
            }
            else
            {
                var userTeams = GetUserTeams(userId);
                var dbTeam = userTeams.FirstOrDefault(team => team.Id.Equals(teamId));
                if (dbTeam != null)
                {
                    _dbSet.Remove(dbTeam);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public IReadOnlyCollection<DbTeam> GetProjectTeams(Guid projectId)
        {
            return _dbSet.Where(t => t.ProjectId.Equals(projectId))
                .Include(team => team.Members)
                .ThenInclude(m => m.Teams)
                .Include(t => t.TaskBoards)
                .ToList()
                .AsReadOnly();
        }
    }
}