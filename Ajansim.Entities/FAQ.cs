using Ajansim.Core.Abstarcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Entities
{
    public class FAQ : BaseEntity
    {
        public string Question { get; set; }                    // Soru
        public string Answer { get; set; }                      // Cevap
    }
}
