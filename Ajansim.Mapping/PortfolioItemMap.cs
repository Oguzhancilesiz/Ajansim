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
    public class PortfolioItemMap : BaseMap<PortfolioItem>
    {
        public override void Configure(EntityTypeBuilder<PortfolioItem> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.Description)
                   .HasMaxLength(1000);

            // PortfolioItem → TeamMember (many-to-one)
            builder.HasOne(x => x.TeamMember)
                   .WithMany(x => x.PortfolioItems)
                   .HasForeignKey(x => x.TeamMemberId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
