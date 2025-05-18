using Ajansim.Core.Abstarcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Entities
{
    public class Service : BaseEntity
    {
        public string Title { get; set; }                       // Hizmet adı
        public string Description { get; set; }                 // Açıklama

        public ICollection<Media> MediaFiles { get; set; }           // Hizmete ait medya
    }
}
