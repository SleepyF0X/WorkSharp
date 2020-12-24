using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WorkSharp.DAL.DbModels
{
    public class DbProject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public List<DbUser> Admins { get; set; }=new List<DbUser>();
        public Guid CreatorId { get; set; }
        public DbUser Creator { get; set; }
        public IReadOnlyCollection<DbTaskBoard> TaskBoards { get; set; }
        public List<DbTeam> Teams { get; set; }
        //public IReadOnlyCollection<DbUser> Members => Teams.SelectMany(team => team.Members).ToList().AsReadOnly();

    }
}
