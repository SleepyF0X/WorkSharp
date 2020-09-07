using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.DbModels.Relations;

namespace WorkSharp.ViewModels.User
{
    public class TeamViewModel
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public IReadOnlyCollection<UserViewModel> Members { get; set; }
    }
}
