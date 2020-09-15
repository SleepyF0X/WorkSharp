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
    public class TeamRepository : ITeamRepository
    {
        private WorkSharpDbContext _context;
        private DbSet<DbTeam> _dbSet;
        public TeamRepository(WorkSharpDbContext context)
        {
            _context = context;
            _dbSet = _context.Teams;
        }

        public void CreateTeam(DbTeam dbTeam)
        {
            _dbSet.Add(dbTeam);
        }

        public void AddMember(Guid teamId, Guid memberId)
        {
            var dbTeam = _dbSet.FirstOrDefault(t => t.Id.Equals(teamId));
            dbTeam.TeamMembers.Add(new DbTeamMembers{TeamId = teamId, MemberId = memberId});
            _context.SaveChanges();
        }

        public IReadOnlyCollection<DbTeam> GetUserTeams(Guid userId)
        {
            return _dbSet.Where(t => t.TeamMembers.Any(s => s.MemberId.Equals(userId))).ToList().AsReadOnly();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public DbTeam GetByIdSecure(object taskBoardId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
