using Ajansim.Core.Abstarcts;
using Ajansim.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Entities
{
    public class User:BaseEntity
    {                        // Anahtar ID
        public string FullName { get; set; }                    // Ad Soyad
        public string Email { get; set; }                       // Giriş için e-posta
        public string PasswordHash { get; set; }                // Şifre (hashlenmiş)
        public UserRole Role { get; set; }                      // Rolü (admin/editor)

        // Navigation – Diğer içerikleri kim oluşturmuş/güncellemiş
        public ICollection<Page> CreatedPages { get; set; }
        public ICollection<Page> UpdatedPages { get; set; }

        public ICollection<BlogPost> CreatedBlogPosts { get; set; }
        public ICollection<BlogPost> UpdatedBlogPosts { get; set; }
    }
}
