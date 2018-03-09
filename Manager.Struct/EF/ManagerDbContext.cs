using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Manager.Core.Enums;
using Manager.Core.Models;

namespace Manager.Struct.EF
{
    public class ManagerDbContext : DbContext
    {
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Attendee> Attendees { get; set; }

        public ManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes()
                                                      .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<Schedule>()
                .ToTable("Schedules");

            builder.Entity<Schedule>()
                .Property(s => s.CreatorId)
                .IsRequired();

            builder.Entity<Schedule>()
                .Property(s => s.CreatedAt)
                .HasDefaultValue(DateTime.Now);

            builder.Entity<Schedule>()
                .Property(s => s.UpdatedAt)
                .HasDefaultValue(DateTime.Now);

            builder.Entity<Schedule>()
                .Property(s => s.Type)
                .HasDefaultValue(ScheduleType.Work);

            builder.Entity<Schedule>()
                .Property(s => s.Status)
                .HasDefaultValue(ScheduleStatus.Valid);

            builder.Entity<Schedule>()
                .HasOne(s => s.Creator)
                .WithMany(c => c.SchedulesCreated);

            builder.Entity<User>()
                .ToTable("Users");

            builder.Entity<User>()
                .Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Entity<Attendee>()
                .ToTable("Attendee");

            builder.Entity<Attendee>()
                .HasOne(a => a.User)
                .WithMany(u => u.SchedulesAttended)
                .HasForeignKey(a => a.UserId);

            builder.Entity<Attendee>()
                .HasOne(a => a.Schedule)
                .WithMany(s => s.Attendees)
                .HasForeignKey(a => a.ScheduleId);

            builder.Entity<Activity>()
                .ToTable("Activities");

            builder.Entity<Activity>()
                .Property(t => t.CreatorId)
                .IsRequired();

            builder.Entity<Activity>()
                .Property(t => t.CreatedAt)
                .HasDefaultValue(DateTime.Now);

            builder.Entity<Activity>()
                .Property(t => t.UpdatedAt)
                .HasDefaultValue(DateTime.Now);

            builder.Entity<Activity>()
                .Property(t => t.Type)
                .HasDefaultValue(ActivityType.Work);

            builder.Entity<Activity>()
                .Property(t => t.Status)
                .HasDefaultValue(ActivityStatus.ToMake);

            builder.Entity<Activity>()
                .Property(t => t.Priority)
                .HasDefaultValue(ActivityPriority.Medium);

            builder.Entity<Activity>()
                .HasOne(t => t.Creator)
                .WithMany(c => c.ActivityCreated);
        }
    }
}
