using Ajansim.Core.Abstarcts;
using Ajansim.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Entities
{
    public class Media : BaseEntity
    {
        public string FileName { get; set; }          // Özgün ad (örnek: belge1.docx)
        public string Url { get; set; }               // Sunucu yolu veya CDN
        public string AltText { get; set; }           // Görseller için alternatif metin (isteğe bağlı)
        public string Extension { get; set; }         // Dosya uzantısı (örnek: .pdf, .png)
        public MediaType MediaType { get; set; }

        // Bağlantılar
        public Guid? PageId { get; set; }
        public Page Page { get; set; }

        public int? ServiceId { get; set; }
        public Service Service { get; set; }

        public int? TeamMemberId { get; set; }
        public TeamMember TeamMember { get; set; }

        public int? PortfolioItemId { get; set; }
        public PortfolioItem PortfolioItem { get; set; }

        public int? BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
