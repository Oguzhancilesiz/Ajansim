using Ajansim.Core.Abstarcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Entities
{
   public class TeamMember:BaseEntity
    {
        public string FullName { get; set; }                    // Ad Soyad
        public string Role { get; set; }                        // Pozisyon (örnek: Grafik Tasarımcı)
        public string Bio { get; set; }                         // Kısa biyografi

        // Sosyal bağlantılar
        public string LinkedIn { get; set; }
        public string GitHub { get; set; }
        public string YouTube { get; set; }
        public string Gmail { get; set; }

        public ICollection<Media> Media { get; set; }           // Profil görselleri
        public ICollection<PortfolioItem> PortfolioItems { get; set; } // Yaptığı işler
    }
}
