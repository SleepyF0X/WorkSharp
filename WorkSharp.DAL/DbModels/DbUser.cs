using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WorkSharp.DAL.DbModels
{
    public class DbUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Skills { get; set; }
        public string Age { get; set; }
        public List<DbTeam> Teams { get; set; } = new List<DbTeam>();
        public List<DbProject> Projects { get; set; } = new List<DbProject>();
        public List<DbProject> AdminProjects { get; set; } = new List<DbProject>();
    }
}