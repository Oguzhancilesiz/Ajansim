using Ajansim.Entities;

namespace Ajansim.WebUI.Areas.Admin.ViewModels
{
    public class PageViewModel
    {
        public Guid ID { get; set; }

        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<Media>? MediaFiles { get; set; }              // mevcut görseller
        public List<IFormFile>? UploadedMedia { get; set; }       // yeni yüklenecek görseller
    }

}
