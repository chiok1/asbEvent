using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using asbEvent.Models;

namespace asbEvent.Data;

public partial class ApplicationDbContext : DbContext
{   
    private readonly IConfiguration _configuration;

    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Attendee> Attendees { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventRegistration> EventRegistrations { get; set; }

    public virtual DbSet<Reward> Rewards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendee>(entity =>
        {
            entity.ToTable("Attendee", "dbo");

            // entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Id)
                 .ValueGeneratedOnAdd();
            entity.Property(e => e.Company).IsUnicode(false);
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.Fname)
                .IsUnicode(false)
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .IsUnicode(false)
                .HasColumnName("LName");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Event", "dbo");

            // entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Id)
                 .ValueGeneratedOnAdd();
            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.EventDesc).IsUnicode(false);
            entity.Property(e => e.EventName).IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<EventRegistration>(entity =>
        {
            entity.ToTable("EventRegistration", "dbo");

            // entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Id)
                 .ValueGeneratedOnAdd();
            entity.Property(e => e.AttendedTime).HasColumnType("datetime");
            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.Qrcode)
                .IsUnicode(false)
                .HasColumnName("QRCode");
            entity.Property(e => e.RegistrationTime).HasColumnType("datetime");

            entity.HasOne(d => d.Attendee).WithMany(p => p.EventRegistrations)
                .HasForeignKey(d => d.AttendeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventRegistration_Attendee");

            entity.HasOne(d => d.Event).WithMany(p => p.EventRegistrations)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventRegistration_Event");
        });

        modelBuilder.Entity<Reward>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Reward", "dbo");

            entity.Property(e => e.Eligibility).IsUnicode(false);
            entity.Property(e => e.RewardDesc).IsUnicode(false);
            entity.Property(e => e.RewardName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
