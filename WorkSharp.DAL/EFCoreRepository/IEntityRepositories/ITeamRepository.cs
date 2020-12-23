using System;
using System.Collections.Generic;
using System.Text;
using WorkSharp.DAL.DbModels;

namespace WorkSharp.DAL.EFCoreRepository.IEntityRepositories
{
    public interface ITeamRepository
    {
        public bool CreateTeam(DbTeam dbTeam, Guid projectId, Guid userId);
        public bool AddMember(Guid teamId, Guid memberId);
        public void RemoveMember(Guid teamId, Guid memberId);
        public IReadOnlyCollection<DbTeam> GetUserTeams(Guid userId);
        public void Save();
        public DbTeam GetByIdSecure(Guid teamId, Guid userId);
        public bool DeleteSecure(Guid teamId, Guid userId);
        public IReadOnlyCollection<DbTeam> GetProjectTeams(Guid projectId);
    }
}
