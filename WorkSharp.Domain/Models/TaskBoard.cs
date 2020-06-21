using System;
using System.Collections.Generic;
using System.Text;

namespace WorkSharp.Domain.Models
{
    public class TaskBoard
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Project Project { get; set; }
        private List<Task> _tasks;
        public IReadOnlyCollection<Task> Tasks => _tasks.AsReadOnly();
    }
}
