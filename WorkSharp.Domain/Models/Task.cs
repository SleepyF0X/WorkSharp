using System;
using System.Collections.Generic;
using System.Text;

namespace WorkSharp.Domain.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public TaskBoard TaskBoard { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public Profile Executor { get; set; }
    }
}
