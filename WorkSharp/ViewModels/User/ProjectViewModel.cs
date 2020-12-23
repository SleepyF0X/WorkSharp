using System;
using System.Collections.Generic;

namespace WorkSharp.ViewModels.User
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public UserViewModel Creator { get; set; }
        public IReadOnlyCollection<TaskBoardViewModel> TaskBoardViewModels { get; set; } 
        public IReadOnlyCollection<TeamViewModel> TeamViewModels { get; set; }
    }
}
