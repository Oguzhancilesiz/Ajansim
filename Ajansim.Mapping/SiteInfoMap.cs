using Ajansim.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Mapping
{
    public class SiteInfoMap : BaseMap<SiteInfo>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SiteInfo> builder)
        {

            builder.Property(x => x.SiteName)
              .IsRequired()
              .HasMaxLength(150);

            builder.Property(x => x.Slogan)
                .HasMaxLength(250);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.Keywords)
                .HasMaxLength(250);

            builder.Property(x => x.Phone)
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasMaxLength(100);

            builder.Property(x => x.Address)
                .HasMaxLength(250);

            builder.Property(x => x.FacebookUrl).HasMaxLength(200);
            builder.Property(x => x.InstagramUrl).HasMaxLength(200);
            builder.Property(x => x.YouTubeUrl).HasMaxLength(200);
            builder.Property(x => x.TwitterUrl).HasMaxLength(200);
            builder.Property(x => x.LinkedInUrl).HasMaxLength(200);

            builder.Property(x => x.FooterText).HasMaxLength(500);

            // Media ile bire-çok ilişki
            builder.HasMany(x => x.MediaFiles)
                .WithOne(m => m.SiteInfo)
                .HasForeignKey(m => m.SiteInfoId)
                .OnDelete(DeleteBehavior.Cascade);


            base.Configure(builder);
        }
    }
}
