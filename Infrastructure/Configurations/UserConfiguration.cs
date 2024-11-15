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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(u => u.UserName).IsRequired();
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.Status).IsRequired();
            builder.Property(u => u.Salt);
            builder.Property(u => u.LastSession);

            
        }
    }
}
