using System;
using System.Collections.Generic;
using System.Text;

namespace WorkSharp.DAL.DbModels.Relations
{
    public class DbTeamMembers
    {
        public Guid TeamId { get; set; }
        public Guid MemberId { get; set; }
        public DbTeam Team { get; set; }
        public DbUser Member { get; set; }
    }
}
