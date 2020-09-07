using System;
using System.Collections.Generic;
using System.Text;
using WorkSharp.DAL.DbModels.Relations;

namespace WorkSharp.DAL.DbModels
{
    public class DbTeam
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public DbProject Project { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public List<DbTeamMembers> TeamMembers { get; set; }
    }
}
