using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Manager.Core.Models;
using Manager.Core.Models.Types;

namespace Manager.Struct.EF
{
    public class ManagerDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

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

            var scheduleBuilder = builder.Entity<Schedule>();

            scheduleBuilder
                .ToTable("Schedules")
                .Property(s => s.CreatorId).IsRequired();

            scheduleBuilder
                .Property(s => s.CreatedAt)
                .HasDefaultValue(DateTime.Now);

            scheduleBuilder
                .Property(s => s.UpdatedAt)
                .HasDefaultValue(DateTime.Now);

            scheduleBuilder
                .Property(s => s.Type)
                .HasDefaultValue(ScheduleType.Work);

            scheduleBuilder
              .Property(s => s.Status)
                .HasDefaultValue(Status.ToComplete);

            scheduleBuilder
                .HasOne(s => s.Creator)
                .WithMany(c => c.SchedulesCreated);

            var userBuilder = builder.Entity<User>();

            userBuilder.ToTable("Users");

            userBuilder
                .Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired();

            userBuilder
                .Property(u => u.FullName)
                .HasMaxLength(150);

            userBuilder
                .Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

            var attendeesBuilder = builder.Entity<Attendee>();

            attendeesBuilder.ToTable("Attendee");

            attendeesBuilder
                .HasOne(a => a.User)
                .WithMany(u => u.SchedulesAttended)
                .HasForeignKey(a => a.UserId);

            attendeesBuilder
                .HasOne(a => a.Schedule)
                .WithMany(s => s.Attendees)
                .HasForeignKey(a => a.ScheduleId);

            var activityBuilder = builder.Entity<Activity>();

            activityBuilder.ToTable("Activities");

            activityBuilder
                .Property(t => t.CreatorId)
                .IsRequired();

            activityBuilder
                .Property(t => t.CreatedAt)
                .HasDefaultValue(DateTime.Now);

            activityBuilder
                .Property(t => t.UpdatedAt)
                .HasDefaultValue(DateTime.Now);

            activityBuilder
                .Property(t => t.Type)
                .HasDefaultValue(ActivityType.Work);

            activityBuilder
                .Property(t => t.Status)
                .HasDefaultValue(Status.ToComplete);

            activityBuilder
                .Property(t => t.Priority)
                .HasDefaultValue(Priority.Medium);

            activityBuilder
                .HasOne(t => t.Creator)
                .WithMany(c => c.ActivitiesCreated);

            var tokensBuilder = builder.Entity<RefreshToken>();

            tokensBuilder.ToTable("Tokens");

            tokensBuilder
                .Property(t => t.UserId)
                .IsRequired();

            tokensBuilder
                .HasOne(t => t.User)
                .WithMany(u => u.Tokens)
                .HasForeignKey(t => t.UserId);
        }
    }
}
