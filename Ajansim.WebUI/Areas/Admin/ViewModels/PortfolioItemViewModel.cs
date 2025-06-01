using Ajansim.Entities;
using Microsoft.AspNetCore.Http;

namespace Ajansim.WebUI.Areas.Admin.ViewModels
{
    public class PortfolioItemViewModel
    {
        public Guid ID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public Guid? TeamMemberId { get; set; }

        public List<Media>? MediaFiles { get; set; }              // Mevcut görseller
        public List<IFormFile>? UploadedMedia { get; set; }       // Yeni yüklenecek

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Dropdownda TeamMember listesi göstermek istersen
        public List<TeamMember>? AvailableTeamMembers { get; set; }
    }
}
