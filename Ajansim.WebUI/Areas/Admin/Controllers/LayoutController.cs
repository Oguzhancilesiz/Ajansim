using Ajansim.Contracts;
using Ajansim.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Ajansim.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LayoutController : Controller
    {
        private readonly IUserService _userFormService;

        public LayoutController(IUserService userFormService)
        {
            _userFormService = userFormService;
        }

        [HttpGet]
        public IActionResult GetUnreadMessageCount()
        {
            var count = _userFormService.GetAll().Count(x => x.Status == Status.Unread);
            return PartialView("_MessageCountPartial", count);
        }
    }
}
