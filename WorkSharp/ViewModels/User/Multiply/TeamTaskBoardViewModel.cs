using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSharp.ViewModels.User.Multiply
{
    public class TeamTaskBoardViewModel
    {
        public TeamViewModel TeamViewModel { get; set; }
        public TaskBoardViewModel TaskBoardViewModel { get; set; }
        public Guid NewAdminId { get; set; }
        public Guid RemoveAdminId { get; set; }
    }
}
