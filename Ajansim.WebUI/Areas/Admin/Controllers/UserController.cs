using Ajansim.Contracts;
using Ajansim.Core.Enums;
using Ajansim.Entities;
using Ajansim.WebUI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Ajansim.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // INDEX
        public IActionResult Index()
        {
            var users = _userService.GetAll()
                .Select(u => new UserViewModel
                {
                    ID = u.ID,
                    FullName = u.FullName,
                    Email = u.Email,
                    Role = u.Role,
                    CreatedAt = u.CreatedAt
                })
                .ToList();

            return View(users);
        }

        // CREATE
        [HttpGet]
        public IActionResult Create()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                var user = new User
                {
                    ID = Guid.NewGuid(),
                    FullName = vm.FullName,
                    Email = vm.Email,
                    PasswordHash = HashPassword(vm.Password),
                    Role = vm.Role,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = Status.Active
                };

                _userService.Add(user);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Hata detayını loglamayı unutma
                ModelState.AddModelError("", "Kullanıcı oluşturulurken bir hata oluştu: " + ex.Message);
                return View(vm);
            }
        }


        // EDIT
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();

            var vm = new UserViewModel
            {
                ID = user.ID,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = _userService.GetById(vm.ID);
            if (user == null) return NotFound();

            user.FullName = vm.FullName;
            user.Email = vm.Email;
            user.Role = vm.Role;
            user.UpdatedAt = DateTime.Now;

            // Eğer yeni şifre girilmişse güncelle
            if (!string.IsNullOrWhiteSpace(vm.Password))
            {
                user.PasswordHash = HashPassword(vm.Password);
            }

            _userService.Update(user);
            return RedirectToAction("Index");
        }

        // SOFT DELETE
        [HttpPost]
        [Route("admin/user/softdelete")]
        public IActionResult SoftDelete([FromBody] Guid id)
        {
            var user = _userService.GetById(id);
            if (user == null) return Json(new { success = false });

            user.Status = Status.Deleted;
            user.UpdatedAt = DateTime.Now;
            _userService.Update(user);

            return Json(new { success = true });
        }

        // 📌 Şifreyi SHA256 hashle (örnek basit çözüm)
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
