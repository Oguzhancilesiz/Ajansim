using Ajansim.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ajansim.WebUI.Areas.Admin.ViewModels
{
    public abstract class BaseViewModel
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public Status Status { get; set; }

        public IFormFileCollection Files { get; set; }

        // Dropdown ihtiyacı varsa
        public Guid CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; } = new();

        // Uyarı/Mesaj göstermek için
        public string AlertMessage { get; set; }
    }
}
