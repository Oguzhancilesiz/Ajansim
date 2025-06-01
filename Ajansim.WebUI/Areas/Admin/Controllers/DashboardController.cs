using Ajansim.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Ajansim.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IBlogPostService _blogPostService;
        private readonly IPortfolioItemService _portfolioService;
        private readonly IUserService _userService;
        private readonly IContactFormService _contactFormService;

        public DashboardController(
            IBlogPostService blogPostService,
            IPortfolioItemService portfolioService,
            IUserService userService,
            IContactFormService contactFormService)
        {
            _blogPostService = blogPostService;
            _portfolioService = portfolioService;
            _userService = userService;
            _contactFormService = contactFormService;
        }

        public IActionResult Index()
        {
            var blogCount = _blogPostService.GetAll().Count();
            var portfolioCount = _portfolioService.GetAll().Count();
            var userCount = _userService.GetAll().Count();
            var unreadMessages = _contactFormService.GetAll().Count(x => x.Status == Core.Enums.Status.Unread);

            ViewBag.BlogCount = blogCount;
            ViewBag.PortfolioCount = portfolioCount;
            ViewBag.UserCount = userCount;
            ViewBag.UnreadMessages = unreadMessages;

            return View();
        }

        // Grafik verileri döndüren endpoint
        [HttpGet]
        public IActionResult GetChartData()
        {
            var labels = new[] { "Blog", "Portfolyo", "Kullanıcı", "Mesaj" };
            var values = new[] {
                _blogPostService.GetAll().Count(),
                _portfolioService.GetAll().Count(),
                _userService.GetAll().Count(),
                _contactFormService.GetAll().Count()
            };

            return Json(new { labels, values });
        }
    }
}
