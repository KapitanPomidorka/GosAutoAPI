using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class FineConfiguration : IEntityTypeConfiguration<Fine>
    {
        public void Configure(EntityTypeBuilder<Fine> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(d => d.Description).IsRequired();
            builder.Property(f => f.Fines).IsRequired();
            builder.Property(p => p.IsPaid).IsRequired();

            builder.HasOne(d => d.Driver)
                .WithMany(f => f.Fines)
                .HasForeignKey(d => d.DriverId);
        }
    }
}
