using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WorkSharp.DAL.DbModels;

namespace WorkSharp.DAL
{
    public class WorkSharpDbContext : IdentityDbContext<DbUser>
    {
        public DbSet<DbProfile> Profiles { get; set; }
        public DbSet<DbProject> Projects { get; set; }
        public DbSet<DbTask> Tasks { get; set; }
        public DbSet<DbTaskBoard> TaskBoards { get; set; }
        public DbSet<DbTeam> Teams { get; set; }
        public WorkSharpDbContext(DbContextOptions<WorkSharpDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<DbProject>().HasData(
                new DbProject { Id = Guid.NewGuid(), Name = "Proj", Info = "OMG" },
                new DbProject { Id = Guid.NewGuid(), Name = "1", Info = "OMG" },
                new DbProject { Id = Guid.NewGuid(), Name = "2", Info = "OMG" },
                new DbProject { Id = Guid.NewGuid(), Name = "3", Info = "OMG" }
                );
        }
    }
}
