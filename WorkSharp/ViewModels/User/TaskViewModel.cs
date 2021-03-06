﻿using System;

namespace WorkSharp.ViewModels.User
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        public Guid TaskBoardId { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public Guid ExecutorId { get; set; }
        public UserViewModel Executor { get; set; }
        public Guid SolutionId { get; set; }
        public SolutionViewModel Solution { get; set; }
    }
}