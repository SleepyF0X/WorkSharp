using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WorkSharp.DAL.DbModels;

namespace WorkSharp.DAL
{
    public class WorkSharpDbContext : IdentityDbContext<DbUser, DbRole, Guid>
    {
        public DbSet<DbProject> Projects { get; set; }
        public DbSet<DbTask> Tasks { get; set; }
        public DbSet<DbTaskBoard> TaskBoards { get; set; }
        public DbSet<DbTeam> Teams { get; set; }
        public DbSet<DbSolution> Solutions { get; set; }

        public WorkSharpDbContext(DbContextOptions<WorkSharpDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DbTaskBoard>()
                .HasOne(u => u.Project)
                .WithMany(u => u.TaskBoards)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DbTeam>()
                .HasMany(p => p.Members)
                .WithMany(a => a.Teams)
                .UsingEntity(j => j.ToTable("TeamMembers"));

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
            modelBuilder.Entity<DbProject>()
                .HasMany(p => p.Admins)
                .WithMany(a => a.Projects)
                .UsingEntity(j => j.ToTable("ProjectAdmins"))
                .HasOne(p => p.Creator)
                .WithMany(u => u.AdminProjects);
            //    pa => pa
            //        .HasOne(pa => pa.Admin)
            //        .WithMany()
            //        .HasForeignKey("AdminId"),
            //    pa => pa
            //        .HasOne(pa => pa.Project)
            //        .WithMany()
            //        .HasForeignKey("ProjectId"))
            //.ToTable("ProjectAdmins")
            //.HasKey(pa => new { pa.AdminId, pa.ProjectId });
        }
    }
}