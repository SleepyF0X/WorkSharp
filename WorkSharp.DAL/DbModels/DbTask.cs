﻿using System;

namespace WorkSharp.DAL.DbModels
{
    public class DbTask
    {
        public Guid Id { get; set; }
        public Guid TaskBoardId { get; set; }
        public DbTaskBoard TaskBoard { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public Guid? ExecutorId { get; set; }
        public DbUser Executor { get; set; }
        public Guid? SolutionId { get; set; }
        public DbSolution Solution { get; set; }
    }
}