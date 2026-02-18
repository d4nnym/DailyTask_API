using DailyTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace DailyTask.Infrastructure.Persistence;

/*public class DailyTaskDbContext : DbContext
{
    public DailyTaskDbContext(DbContextOptions<DailyTaskDbContext> options) : base(options) { }*/
public class DailyTaskDbContext(DbContextOptions<DailyTaskDbContext> options) : DbContext(options) {
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("DAILYTASK");

        modelBuilder.Entity<Project>(e =>
        {
            e.ToTable("PROJECTS");
            e.HasKey(x => x.Id);

            e.Property(x => x.Id).HasColumnName("ID");
            e.Property(x => x.Name).HasColumnName("NAME").HasMaxLength(255).IsRequired();
            e.Property(x => x.Description).HasColumnName("DESCRIPTION").HasMaxLength(2000);
            e.Property(x => x.CreatedAtUtc).HasColumnName("CREATED_AT_UTC");

            e.HasMany(x => x.Tasks)
             .WithOne(x => x.Project)
             .HasForeignKey(x => x.ProjectId);
        });

        modelBuilder.Entity<TaskItem>(e =>
        {
            e.ToTable("TASKS");
            e.HasKey(x => x.Id);

            e.Property(x => x.Id).HasColumnName("ID");
            e.Property(x => x.ProjectId).HasColumnName("PROJECT_ID");

            e.Property(x => x.Title).HasColumnName("TITLE").HasMaxLength(255).IsRequired();
            e.Property(x => x.Notes).HasColumnName("NOTES").HasMaxLength(4000);

            e.Property(x => x.IsDone).HasColumnName("IS_DONE");
            e.Property(x => x.CreatedAtUtc).HasColumnName("CREATED_AT_UTC");
            e.Property(x => x.DueAtUtc).HasColumnName("DUE_AT_UTC");
        });
    }
}