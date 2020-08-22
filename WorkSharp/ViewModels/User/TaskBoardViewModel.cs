using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSharp.ViewModels.User
{
    public class TaskBoardViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProjectId { get; set; }
        public string Type { get; set; }
        public IReadOnlyCollection<TaskViewModel> TaskViewModels { get; set; }
    }
}
