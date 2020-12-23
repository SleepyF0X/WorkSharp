using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkSharp.DAL.DbModels
{
    public class DbTeam
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public DbProject Project { get; set; }
        public Guid CreatorId { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Status { get; set; }
        public List<DbUser> Members { get; set; } = new List<DbUser>();
        public List<DbTaskBoard> TaskBoards { get; set; } = new List<DbTaskBoard>();
    }
}
