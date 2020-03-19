using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainingFinder.Models;

namespace TrainingFinder.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TrainingAppUser>()
                .HasKey(c => new { c.AppUserId, c.TrainingId });
            builder.Entity<TrainingAppUser>()
                .HasOne(c => c.Training)
                .WithMany(d => d.TrainingAppUsers)
                .HasForeignKey(c => c.TrainingId);
            builder.Entity<TrainingAppUser>()
                .HasOne(c => c.AppUser)
                .WithMany(d => d.TrainingAppUsers)
                .HasForeignKey(c => c.AppUserId);
        }

        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<TrainingAppUser> TrainingAppUsers { get; set; }
    }
}
