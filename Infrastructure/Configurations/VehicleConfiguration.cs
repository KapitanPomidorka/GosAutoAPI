using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(n => n.CarNumbers).IsRequired();
            builder.Property(d => d.Description);
            builder.Property(m => m.Model).IsRequired();

            builder.HasOne(d => d.Driver)
                .WithMany(v => v.Vehicles)
                .HasForeignKey(x => x.DriverId);
        }
    }
}
