using Ajansim.Core.Abstarcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Entities
{
    public class Page : BaseEntity
    {
        public string Title { get; set; }                       // Sayfa başlığı (örnek: Hakkımızda)
        public string Slug { get; set; }                        // URL dostu ad (örnek: hakkimizda)
        public string SubTitle { get; set; }                    // Sayfa alt başlığı
        public string Content { get; set; }                     // İçerik (HTML/metin)
        public string MetaDescription { get; set; }                     // İçerik (HTML/metin)
        public string MetaKeyword { get; set; }                     // İçerik (HTML/metin)

        public ICollection<Media> MediaFiles { get; set; }           // Sayfaya bağlı görseller/videolar
    }
}
