using Ajansim.Core.Abstarcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Entities
{
    public class BlogPost:BaseEntity
    {
        public string Title { get; set; }                       // Başlık
        public string Summary { get; set; }                     // Kısa özet
        public string Content { get; set; }                     // Detaylı içerik
        public DateTime PublishedAt { get; set; }               // Yayınlanma tarihi

        public Guid CategoryId { get; set; }                     // Kategori
        public Category Category { get; set; }

        public List<Media> MediaFiles { get; set; }           // Blog’a ait görseller
    }
}
