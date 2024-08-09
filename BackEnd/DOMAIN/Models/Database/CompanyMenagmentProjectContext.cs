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

    public virtual DbSet<CommunicationMessage> CommunicationMessages { get; set; }

    public virtual DbSet<ShiftType> ShiftTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCommunication> UserCommunications { get; set; }

    public virtual DbSet<WorkCalendar> WorkCalendars { get; set; }

    public virtual DbSet<WorkerType> WorkerTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-6F991P0;Database=CompanyMenagmentProject;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommunicationMessage>(entity =>
        {
            entity.Property(e => e.Message).HasMaxLength(250);

            entity.HasOne(d => d.Communication).WithMany(p => p.CommunicationMessages)
                .HasForeignKey(d => d.CommunicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommunicationMessages_UserCommunication");

            entity.HasOne(d => d.Sender).WithMany(p => p.CommunicationMessages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommunicationMessages_Users");
        });

        modelBuilder.Entity<ShiftType>(entity =>
        {
            entity.HasKey(e => e.ShiftNumber);

            entity.Property(e => e.EndTime)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.StartTime)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();
        });

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

        modelBuilder.Entity<UserCommunication>(entity =>
        {
            entity.ToTable("UserCommunication");

            entity.HasIndex(e => new { e.User1, e.User2 }, "IX_UniquePair").IsUnique();

            entity.HasOne(d => d.User1Navigation).WithMany(p => p.UserCommunicationUser1Navigations)
                .HasForeignKey(d => d.User1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCommunication_Users1");

            entity.HasOne(d => d.User2Navigation).WithMany(p => p.UserCommunicationUser2Navigations)
                .HasForeignKey(d => d.User2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCommunication_Users2");
        });

        modelBuilder.Entity<WorkCalendar>(entity =>
        {
            entity.HasKey(e => new { e.Date, e.Shift, e.UserId });

            entity.ToTable("WorkCalendar");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.ShiftNavigation).WithMany(p => p.WorkCalendars)
                .HasForeignKey(d => d.Shift)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkCalendar_ShiftTypes");

            entity.HasOne(d => d.User).WithMany(p => p.WorkCalendars)
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
