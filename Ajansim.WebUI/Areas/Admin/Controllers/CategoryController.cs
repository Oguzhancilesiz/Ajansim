using Ajansim.Contracts;
using Ajansim.Core.Enums;
using Ajansim.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ajansim.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService  = categoryService;
        }


        public IActionResult Index()
        {
            var list = _categoryService.GetAll().ToList();
            if (list == null)
            {
                ViewBag.Error = "Veritabanında kategori bulunamadı";
                return View();
            }
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);

            }

            category.ID = Guid.NewGuid();
            category.CreatedAt = DateTime.Now;
            category.UpdatedAt = DateTime.Now;
            category.Status = Status.Active;

            _categoryService.Add(category);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            var existing = _categoryService.GetById(category.ID);
            if (existing == null)
                return NotFound();

            existing.Name = category.Name;
            existing.UpdatedAt = DateTime.Now;

            _categoryService.Update(existing);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
                return NotFound();

            category.Status = Status.Deleted;
            category.UpdatedAt = DateTime.Now;

            _categoryService.Update(category);
            return RedirectToAction("Index");
        }

    }
}
