using System;
using System.Collections.Generic;
using System.Text;
using WorkSharp.DAL.DbModels;

namespace WorkSharp.DAL.EFCoreRepository.IEntityRepositories
{
    public interface ITeamRepository
    {
        public void CreateTeam(DbTeam dbTeam);
        public void CreateTeam(string name, string info, Guid projectId);
        public void AddMember(Guid teamId, Guid memberId);
        public IReadOnlyCollection<DbTeam> GetUserTeams(Guid userId);
        public void Save();
    }
}
