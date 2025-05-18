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
    public class PageMap : BaseMap<Page>
    {
        public override void Configure(EntityTypeBuilder<Page> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(100);
            builder.Property(x => x.MetaDescription).HasMaxLength(200);
            builder.Property(x => x.MetaKeyword).HasMaxLength(200);
            builder.Property(x => x.SubTitle).HasMaxLength(200);


            //navigatiıon property
            builder.HasMany(x => x.MediaFiles)
              .WithOne(x => x.Page)
              .HasForeignKey(x => x.PageId)
              .OnDelete(DeleteBehavior.Cascade); // Page silinince Medialar da silinir
        }
    }
}
