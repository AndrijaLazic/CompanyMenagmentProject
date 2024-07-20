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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-6F991P0;Database=CompanyMenagmentProject;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "UQ_Email").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ_PhoneNumber").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Lastname).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.PhoneNumber).HasMaxLength(30);

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
