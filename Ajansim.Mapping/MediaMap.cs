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
    public class MediaMap : BaseMap<Media>
    {
        public override void Configure(EntityTypeBuilder<Media> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FileName)
                   .IsRequired().HasMaxLength(200);

            builder.Property(x => x.Url)
                   .IsRequired().HasMaxLength(500);

            builder.Property(x => x.AltText)
                   .HasMaxLength(200);

            builder.Property(x => x.Extension)
                   .HasMaxLength(10);

            builder.Property(x => x.MediaType)
                   .IsRequired();

            builder.HasOne(x => x.Page)
                   .WithMany(x => x.MediaFiles)
                   .HasForeignKey(x => x.PageId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Service)
                   .WithMany(x => x.MediaFiles)
                   .HasForeignKey(x => x.ServiceId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.TeamMember)
                   .WithMany(x => x.MediaFiles)
                   .HasForeignKey(x => x.TeamMemberId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PortfolioItem)
                   .WithMany(x => x.MediaFiles)
                   .HasForeignKey(x => x.PortfolioItemId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.BlogPost)
                   .WithMany(x => x.MediaFiles)
                   .HasForeignKey(x => x.BlogPostId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}