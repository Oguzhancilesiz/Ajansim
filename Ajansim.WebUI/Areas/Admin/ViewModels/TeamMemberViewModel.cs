using Ajansim.Entities;
using Microsoft.AspNetCore.Http;

namespace Ajansim.WebUI.Areas.Admin.ViewModels
{
    public class TeamMemberViewModel
    {
        public Guid ID { get; set; }

        public string FullName { get; set; }
        public string Role { get; set; }
        public string Bio { get; set; }

        // Sosyal bağlantılar
        public string LinkedIn { get; set; }
        public string GitHub { get; set; }
        public string YouTube { get; set; }
        public string Gmail { get; set; }

        // Medya
        public List<IFormFile>? UploadedMedia { get; set; }        // Yeni yüklenen görseller
        public List<Media>? MediaFiles { get; set; }               // Mevcut görseller

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
