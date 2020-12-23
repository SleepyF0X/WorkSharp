using System;
using System.Collections.Generic;
using System.Text;

namespace WorkSharp.DAL.DbModels
{
    public class DbTaskBoard
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProjectId { get; set; }
        public DbProject Project { get; set; }
        public Guid TeamId { get; set; }
        public DbTeam Team { get; set; }
        public string Type { get; set; }
        public IReadOnlyCollection<DbTask> Tasks { get; set; }
    }
}
