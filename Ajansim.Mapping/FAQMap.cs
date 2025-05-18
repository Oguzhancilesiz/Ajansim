using Ajansim.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Mapping
{
    public class FAQMap: BaseMap<FAQ>
    {
        public override void Configure(EntityTypeBuilder<FAQ> builder)
        {
            builder.Property(x => x.Question).IsRequired().HasMaxLength(200); // Soru
            builder.Property(x => x.Answer).IsRequired().HasMaxLength(1000); // Cevap
            base.Configure(builder);
        }
    }
}
