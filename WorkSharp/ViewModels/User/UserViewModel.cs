using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WorkSharp.ViewModels.User
{
    public class UserViewModel:IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Skills { get; set; }
        public string Age { get; set; }
        public List<TeamViewModel> Teams { get; set; }
        public List<ProjectViewModel> Projects { get; set; } = new List<ProjectViewModel>();
        public List<ProjectViewModel> AdminProjects { get; set; } = new List<ProjectViewModel>();
    }
}
