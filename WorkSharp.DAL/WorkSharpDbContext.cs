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
        public DbSet<DbProfile> Profiles { get; set; }
        public DbSet<DbProject> Projects { get; set; }
        public DbSet<DbTask> Tasks { get; set; }
        public DbSet<DbTaskBoard> TaskBoards { get; set; }
        public DbSet<DbTeam> Teams { get; set; }
        public DbSet<DbUser> Users { get; set; }
        public WorkSharpDbContext(DbContextOptions<WorkSharpDbContext> options) : base(options) { }
    }
}
