using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TrainingFinder.Entities;
using TrainingFinder.Models;
using TrainingFinder.Models.Users;

namespace TrainingFinder.Data
{
    public class ApplicationDbContext : IdentityDbContext<AdminUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TrainingUser>()
                .HasKey(c => new { c.UserId, c.TrainingId });
            builder.Entity<TrainingUser>()
                .HasOne(c => c.Training)
                .WithMany(d => d.TrainingUsers)
                .HasForeignKey(c => c.TrainingId);
            builder.Entity<TrainingUser>()
                .HasOne(c => c.User)
                .WithMany(d => d.TrainingUsers)
                .HasForeignKey(c => c.UserId);            
            builder.Entity<Training>()
                .HasOne(c => c.Gym)
                .WithMany(d => d.Trainings)
                .HasForeignKey(d => d.GymId);
            builder.Entity<User>()
                .HasMany(u => u.BodyDimensions)
                .WithOne(b => b.User);
        }

        public DbSet<AdminUser> AdminUsers { get; set; }
        public new DbSet<Entities.User> Users { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<TrainingUser> TrainingUsers { get; set; }
        public DbSet<BodyDimension> BodyDimensions { get; set; }
    }
}
