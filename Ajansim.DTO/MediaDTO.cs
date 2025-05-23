using Ajansim.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.DTO
{
    public class MediaDTO
    {
        public Guid ID { get; set; }
        public string Url { get; set; }
        public string AltText { get; set; }
        public string Extension { get; set; }
        public MediaType MediaType { get; set; } // string olarak kullanımı UI'da kolay
    }
}
