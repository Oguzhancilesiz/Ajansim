using Ajansim.Contracts;
using Ajansim.Services;
using Ajansim.WebUI.Views.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ajansim.WebUI.Controllers
{
    [Area("")] // Public alan
    public class HomeController : Controller
    {
        private readonly IService _serviceService;
        private readonly ITeamMemberService _teamMemberService;
        private readonly IPortfolioItemService _portfolioItemService;
        private readonly IFAQService _faqService;
        private readonly IPageService _pageService;
        private readonly IMediaService _mediaService;

        public HomeController(
            IService serviceService,
            ITeamMemberService teamMemberService,
            IPortfolioItemService portfolioItemService,
            IFAQService faqService,
            IPageService pageService,
            IMediaService mediaService)
        {
            _serviceService = serviceService;
            _teamMemberService = teamMemberService;
            _portfolioItemService = portfolioItemService;
            _faqService = faqService;
            _pageService = pageService;
            _mediaService = mediaService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                //Services = _serviceService.GetAll().ToList(),
                //TeamMembers = _teamMemberService.GetAll().ToList(),
                //PortfolioItems = _portfolioItemService.GetAll().ToList(),
                //FAQs = _faqService.GetAll().ToList(),
                //AboutPage = _pageService.GetBySlug("hakkimizda"),
                //ContactPage = _pageService.GetBySlug("iletisim"),
                //IndexPage = _pageService.GetBySlug("anasayfa"),
                //SiteLogo = await _mediaService.GetLogoMediaAsync()
            };

            return View(model);
        }
    }

}
