using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DOMAIN.Models.Database;

public partial class CompanyMenagmentProjectContext : DbContext
{
    public CompanyMenagmentProjectContext()
    {
    }

    public CompanyMenagmentProjectContext(DbContextOptions<CompanyMenagmentProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WorkCalendar> WorkCalendars { get; set; }

    public virtual DbSet<WorkerType> WorkerTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.PhoneNumber, "UQ__Users__85FB4E384AEF2402").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534ABD18CD0").IsUnique();

            entity.HasOne(d => d.WorkerTypeNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.WorkerType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_WorkerTypes");
        });

        modelBuilder.Entity<WorkCalendar>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("WorkCalendar");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkCalendar_Users");
        });

        modelBuilder.Entity<WorkerType>(entity =>
        {
            entity.Property(e => e.TypeName)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
