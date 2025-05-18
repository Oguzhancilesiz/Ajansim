using Ajansim.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Mapping
{
   public class ContactFormMap: BaseMap<ContactForm>
    {
        public override void Configure(EntityTypeBuilder<ContactForm> builder)
        {
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(100); // Ad Soyad
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100); // E-posta
            builder.Property(x => x.Message).IsRequired().HasMaxLength(500); // Mesaj içeriği
            base.Configure(builder);
        }
    }
}
