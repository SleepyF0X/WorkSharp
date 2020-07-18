using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSharp.ViewModels
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public Guid CreatorId { get; set; }
    }
}
