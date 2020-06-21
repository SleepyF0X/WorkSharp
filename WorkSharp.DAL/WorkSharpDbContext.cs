using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WorkSharp.DAL.DbModels;

namespace WorkSharp.DAL
{
    public class WorkSharpDbContext : IdentityDbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskBoard> TaskBoards { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public WorkSharpDbContext(DbContextOptions<WorkSharpDbContext> options) : base(options) { }
    }
}
