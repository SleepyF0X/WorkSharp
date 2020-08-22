using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using WorkSharp.DAL.DbModels;

namespace WorkSharp.DAL
{
    public class WorkSharpDbContext : IdentityDbContext<DbUser, DbRole, Guid>
    {
        public DbSet<DbProject> Projects { get; set; }
        public DbSet<DbTask> Tasks { get; set; }
        public DbSet<DbTaskBoard> TaskBoards { get; set; }
        public DbSet<DbTeam> Teams { get; set; }
        public WorkSharpDbContext(DbContextOptions<WorkSharpDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DbTaskBoard>()
                .HasOne(u => u.Project)
                .WithMany(u => u.TaskBoards)
                .HasForeignKey(p => p.ProjectId);
        }
    }
}
