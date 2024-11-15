using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(n => n.Name).IsRequired();
            builder.Property(nod => nod.NumberDocuments).IsRequired();
            builder.Property(d => d.Description);
            builder.Property(f => f.Forfeit);
            builder.Property(fo => fo.CountForfeit);

            builder.HasMany(v => v.Vehicles)
                .WithOne(d => d.Driver);
        }
    }
}
