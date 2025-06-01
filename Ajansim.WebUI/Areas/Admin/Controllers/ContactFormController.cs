using Ajansim.Contracts;
using Ajansim.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Ajansim.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactFormController : Controller
    {
        private readonly IContactFormService _contactFormService;

        public ContactFormController(IContactFormService contactFormService)
        {
            _contactFormService = contactFormService;
        }

        // INDEX - Listeleme
        public IActionResult Index(string filter = "all")
        {
            var forms = _contactFormService.GetAll();

            if (filter == "unread")
                forms = forms.Where(x => x.Status == Status.Active);

            var result = forms.OrderByDescending(x => x.CreatedAt).ToList();

            ViewBag.Filter = filter;
            return View(result);
        }

        [HttpPost]
        [Route("admin/contactform/markasread")]
        public IActionResult MarkAsRead([FromBody] Guid id)
        {
            var form = _contactFormService.GetById(id);
            if (form == null)
                return Json(new { success = false });

            form.Status = Status.Read; // veya Status.Read gibi özel enum da tanımlayabilirsin
            form.UpdatedAt = DateTime.Now;
            _contactFormService.Update(form);

            return Json(new { success = true });
        }

    }
}
