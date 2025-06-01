using Ajansim.Core.Abstarcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Entities
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }                  // Marka adı (örnek: Nike)
        public string Website { get; set; }               // Marka web adresi (opsiyonel)
        public string Description { get; set; }           // Kısa açıklama (isteğe bağlı)

        public ICollection<Media> MediaFiles { get; set; }

    }
}
