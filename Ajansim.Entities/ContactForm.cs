using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Entities
{
   public class ContactForm
    {

        public string FullName { get; set; }                    // Ad Soyad
        public string Email { get; set; }                       // E-posta
        public string Message { get; set; }                     // Mesaj içeriği
    }
}
