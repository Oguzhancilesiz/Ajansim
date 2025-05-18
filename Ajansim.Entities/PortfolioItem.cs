using Ajansim.Core.Abstarcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Entities
{
   public class PortfolioItem:BaseEntity
    {
        public string Title { get; set; }                       // Proje başlığı
        public string Description { get; set; }                 // Açıklama

        public int TeamMemberId { get; set; }                   // Hangi ekip üyesine ait
        public TeamMember TeamMember { get; set; }

        public ICollection<Media> Media { get; set; }           // Proje görselleri
    }
}
