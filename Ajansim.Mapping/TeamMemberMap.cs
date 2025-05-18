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
    public class TeamMemberMap : BaseMap<TeamMember>
    {
        public override void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FullName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Role)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Bio)
                   .HasMaxLength(1000); // Uzun biyografi olabilir

            builder.Property(x => x.LinkedIn)
                   .HasMaxLength(200);

            builder.Property(x => x.GitHub)
                   .HasMaxLength(200);

            builder.Property(x => x.YouTube)
                   .HasMaxLength(200);

            builder.Property(x => x.Gmail)
                   .HasMaxLength(100);

            // TeamMember → Media (1 - N)
            builder.HasMany(x => x.MediaFiles)
                   .WithOne(x => x.TeamMember)
                   .HasForeignKey(x => x.TeamMemberId)
                   .OnDelete(DeleteBehavior.Cascade);

            // TeamMember → PortfolioItem (1 - N)
            builder.HasMany(x => x.PortfolioItems)
                   .WithOne(x => x.TeamMember)
                   .HasForeignKey(x => x.TeamMemberId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
