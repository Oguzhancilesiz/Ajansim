using Ajansim.Core.Abstarcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Entities
{
    public class SiteInfo : BaseEntity
    {
        // Genel
        public string SiteName { get; set; }             // Site başlığı (title tag)
        public string Slogan { get; set; }               // Ana başlık
        public string Description { get; set; }          // Açıklama (meta description)
        public string Keywords { get; set; }             // SEO anahtar kelimeler

        // İletişim
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // Sosyal Medya (isteğe bağlı)
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string YouTubeUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string LinkedInUrl { get; set; }

        public ICollection<Media> MediaFiles { get; set; }


        // Footer metni gibi alanlar da eklenebilir
        public string FooterText { get; set; }
    }
}
