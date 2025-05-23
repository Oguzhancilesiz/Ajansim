using Ajansim.Core.Abstarcts;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Entities
{
    public class BlogPost : BaseEntity
    {
        public string Title { get; set; }                       // Başlık
        public string Summary { get; set; }                     // Kısa özet
        public string Content { get; set; }                     // Detaylı içerik
        public DateTime PublishedAt { get; set; }
        // Yayınlanma tarihi
        [Required(ErrorMessage = "Kategori seçiniz")]
        public Guid CategoryId { get; set; }                     // Kategori
        [ValidateNever]
        public Category Category { get; set; }
        public List<Media>? MediaFiles { get; set; }           // Blog’a ait görseller
    }
}
