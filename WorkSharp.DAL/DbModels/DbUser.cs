using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using WorkSharp.DAL.DbModels.Relations;

namespace WorkSharp.DAL.DbModels
{
    public class DbUser : IdentityUser<Guid>
    {
        public List<DbTeamMembers> TeamMembers { get; set; }
    }
}
