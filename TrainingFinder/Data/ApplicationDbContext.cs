using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        }

        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Entities.User> Users { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<TrainingUser> TrainingUsers { get; set; }
    }
}
