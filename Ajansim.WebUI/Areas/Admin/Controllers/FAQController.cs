using Ajansim.Contracts;
using Ajansim.Core.Enums;
using Ajansim.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ajansim.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FAQController : Controller
    {
        private readonly IFAQService _faqService;

        public FAQController(IFAQService faqService)
        {
            _faqService = faqService;
        }

        // INDEX
        public IActionResult Index()
        {
            var list = _faqService.GetAll().ToList();
            return View(list);
        }

        // CREATE
        [HttpGet]
        public IActionResult Create()
        {
            return View(new FAQ());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FAQ model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.ID = Guid.NewGuid();
            model.Status = Status.Active;
            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = DateTime.Now;

            _faqService.Add(model);
            return RedirectToAction("Index");
        }

        // EDIT
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var faq = _faqService.GetById(id);
            if (faq == null) return NotFound();

            return View(faq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FAQ model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var faq = _faqService.GetById(model.ID);
            if (faq == null) return NotFound();

            faq.Question = model.Question;
            faq.Answer = model.Answer;
            faq.UpdatedAt = DateTime.Now;

            _faqService.Update(faq);
            return RedirectToAction("Index");
        }

        // SOFT DELETE
        [HttpPost]
        [Route("admin/faq/softdelete")]
        public IActionResult SoftDelete([FromBody] Guid id)
        {
            var faq = _faqService.GetById(id);
            if (faq == null) return Json(new { success = false });

            faq.Status = Status.Deleted;
            faq.UpdatedAt = DateTime.Now;
            _faqService.Update(faq);

            return Json(new { success = true });
        }
    }
}
