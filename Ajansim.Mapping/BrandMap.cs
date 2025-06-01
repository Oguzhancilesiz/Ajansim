using Ajansim.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Mapping
{
    public class BrandMap : BaseMap<Brand>
    {
        public override void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(x => x.Name)
              .IsRequired()
              .HasMaxLength(150);

            builder.Property(x => x.Website)
                .HasMaxLength(250);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            // Media ile bire-çok ilişki
            builder.HasMany(x => x.MediaFiles)
                   .WithOne(m => m.Brand)
                   .HasForeignKey(m => m.BrandId)
                   .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
