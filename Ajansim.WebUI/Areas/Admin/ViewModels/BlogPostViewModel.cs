using Ajansim.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class BlogPostViewModel
{
    public Guid ID { get; set; }

    [Required(ErrorMessage = "Başlık zorunludur")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Özet zorunludur")]
    public string Summary { get; set; }

    [Required(ErrorMessage = "İçerik zorunludur")]
    public string Content { get; set; }

    [Required(ErrorMessage = "Yayın tarihi zorunludur")]
    public DateTime PublishedAt { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Kategori seçilmelidir")]
    public Guid CategoryId { get; set; }

    public List<SelectListItem>? CategoryList { get; set; } // DropDown için

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public List<Media>? MediaFiles { get; set; }              // mevcut görseller
    public List<IFormFile>? UploadedMedia { get; set; }       // yeni yüklenecek görseller
}
