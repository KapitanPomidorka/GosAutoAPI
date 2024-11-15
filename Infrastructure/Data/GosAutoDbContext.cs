using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Configurations;

namespace Infrastructure.Data
{
    public class GosAutoDbContext : DbContext
    {
        public GosAutoDbContext(DbContextOptions<GosAutoDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Driver> DriversTable { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
            modelBuilder.ApplyConfiguration(new DriverConfiguration());
            modelBuilder.ApplyConfiguration(new FineConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
