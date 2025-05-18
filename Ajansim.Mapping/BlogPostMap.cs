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
   public class BlogPostMap:BaseMap<BlogPost>
    {
        public override void Configure(EntityTypeBuilder<BlogPost> builder)
        {

            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Summary).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.PublishedAt).IsRequired();

            // Blog → Media (1-many)
            builder.HasMany(x => x.MediaFiles)
                .WithOne(x => x.BlogPost)
                .HasForeignKey(x => x.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Blog → Category (many-to-1)
            builder.HasOne(x => x.Category)
                .WithMany(c => c.BlogPosts)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // isteğe bağlı: Cascade yerine Restrict de olabilir
        }
    }
}
