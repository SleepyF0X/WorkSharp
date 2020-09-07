using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WorkSharp.DAL.DbModels
{
    public class DbProject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Info { get; set; }
        //public Guid CreatorId { get; set; }
        public IReadOnlyCollection<DbTaskBoard> TaskBoards { get; set; }
        public List<DbTeam> Teams { get; set; }
        //public IReadOnlyCollection<DbUser> Members => Teams.SelectMany(team => team.TeamMembers.Select(tm => tm.Member)).ToList().AsReadOnly();

        public IReadOnlyCollection<DbUser> Members()
        {
            return Teams.SelectMany(team => team.TeamMembers.Select(tm => tm.Member)).ToList().AsReadOnly();
        }
    }
}
