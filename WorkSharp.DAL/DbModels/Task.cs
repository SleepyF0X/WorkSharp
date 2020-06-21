using System;
using System.Collections.Generic;
using System.Text;

namespace WorkSharp.DAL.DbModels
{
    public class Task
    {
        public Guid Id { get; set; }
        public Guid TaskBoardId { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public Guid ExecutorId { get; set; }
    }
}
