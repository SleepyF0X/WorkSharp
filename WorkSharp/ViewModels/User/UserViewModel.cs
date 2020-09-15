using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WorkSharp.ViewModels.User
{
    public class UserViewModel:IdentityUser<Guid>
    {
        public List<TeamViewModel> Teams { get; set; }
    }
}
