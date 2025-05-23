using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ajansim.WebUI.Areas.Admin.ViewModels
{
    public class BlogPostViewModel
    {
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public string? Content { get; set; }
        public DateTime? PublishedAt { get; set; }

        // Edit için mevcut görseller (opsiyonel)
        public List<string> ExistingMediaUrls { get; set; } = new();
        public Guid CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; } = new();
    }
}
