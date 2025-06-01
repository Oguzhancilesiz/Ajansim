using Ajansim.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ajansim.WebUI.Areas.Admin.ViewModels
{
    public class UserViewModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        // Şifre sadece Create işleminde zorunlu, Edit'te opsiyonel olacak
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalı.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Rol seçimi zorunludur.")]
        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
