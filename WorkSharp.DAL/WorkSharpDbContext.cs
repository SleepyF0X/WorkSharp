using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.DbModels.Relations;

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
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<DbTeamMembers>()
                .HasKey(param => new {param.MemberId, param.TeamId});
            modelBuilder.Entity<DbTeamMembers>()
                .HasOne(tm => tm.Member)
                .WithMany(m => m.TeamMembers)
                .HasForeignKey(tm => tm.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<DbTeamMembers>()
                .HasOne(tm => tm.Team)
                .WithMany(m => m.TeamMembers)
                .HasForeignKey(tm => tm.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<DbTask>()
                .HasOne(t => t.TaskBoard)
                .WithMany(tb => tb.Tasks)
                .HasForeignKey(t => t.TaskBoardId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<DbTeam>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Teams)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
