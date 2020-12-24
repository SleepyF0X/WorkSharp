using System;
using System.Collections.Generic;

namespace WorkSharp.ViewModels.User
{
    public class TeamViewModel
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public IReadOnlyCollection<UserViewModel> Members { get; set; }
        public IReadOnlyCollection<TaskBoardViewModel> TaskBoards { get; set; }
        public Guid MemberId { get; set; }
        public bool Admin { get; set; }
    }
}
