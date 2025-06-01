using Ajansim.Entities;
using Microsoft.AspNetCore.Http;

namespace Ajansim.WebUI.Areas.Admin.ViewModels
{
    public class ServiceViewModel
    {
        public Guid ID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public List<Media>? MediaFiles { get; set; }            // Mevcut medya
        public List<IFormFile>? UploadedMedia { get; set; }     // Yeni eklenecek dosyalar

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
