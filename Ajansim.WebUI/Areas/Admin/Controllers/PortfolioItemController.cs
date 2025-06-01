using Ajansim.Contracts;
using Ajansim.Core.Enums;
using Ajansim.Entities;
using Ajansim.WebUI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ajansim.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioItemController : Controller
    {
        private readonly IPortfolioItemService _portfolioService;
        private readonly IMediaService _mediaService;
        private readonly ITeamMemberService _teamMemberService;
        private readonly IWebHostEnvironment _env;

        public PortfolioItemController(
            IPortfolioItemService portfolioService,
            IMediaService mediaService,
            ITeamMemberService teamMemberService,
            IWebHostEnvironment env)
        {
            _portfolioService = portfolioService;
            _mediaService = mediaService;
            _teamMemberService = teamMemberService;
            _env = env;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            var items = _portfolioService.GetAll().ToList();

            var vmList = new List<PortfolioItemViewModel>();

            foreach (var item in items)
            {
                var media = await _mediaService.GetMediaByEntityAsync(item.ID, "PortfolioItem");

                vmList.Add(new PortfolioItemViewModel
                {
                    ID = item.ID,
                    Title = item.Title,
                    Description = item.Description,
                    TeamMemberId = item.TeamMemberId,
                    CreatedAt = item.CreatedAt,
                    MediaFiles = media
                });
            }

            return View(vmList);
        }

        // CREATE
        [HttpGet]
        public IActionResult Create()
        {
            return View(new PortfolioItemViewModel
            {
                AvailableTeamMembers = _teamMemberService.GetAll().ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PortfolioItemViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.AvailableTeamMembers = _teamMemberService.GetAll().ToList();
                return View(vm);
            }

            var entity = new PortfolioItem
            {
                ID = Guid.NewGuid(),
                Title = vm.Title,
                Description = vm.Description,
                TeamMemberId = vm.TeamMemberId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = Status.Active
            };

            _portfolioService.Add(entity);

            if (vm.UploadedMedia != null && vm.UploadedMedia.Any())
            {
                await _mediaService.UploadMediaAsyncFromList(
                    vm.UploadedMedia,
                    MediaType.Image,
                    _env.WebRootPath,
                    portfolioItemId: entity.ID
                );
            }

            return RedirectToAction("Index");
        }

        // EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var item = _portfolioService.GetById(id);
            if (item == null) return NotFound();

            var media = await _mediaService.GetMediaByEntityAsync(id, "PortfolioItem");

            var vm = new PortfolioItemViewModel
            {
                ID = item.ID,
                Title = item.Title,
                Description = item.Description,
                TeamMemberId = item.TeamMemberId,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt,
                MediaFiles = media,
                AvailableTeamMembers = _teamMemberService.GetAll().ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PortfolioItemViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.AvailableTeamMembers = _teamMemberService.GetAll().ToList();
                vm.MediaFiles = await _mediaService.GetMediaByEntityAsync(vm.ID, "PortfolioItem");
                return View(vm);
            }

            var item = _portfolioService.GetById(vm.ID);
            if (item == null) return NotFound();

            item.Title = vm.Title;
            item.Description = vm.Description;
            item.TeamMemberId = vm.TeamMemberId;
            item.UpdatedAt = DateTime.Now;

            _portfolioService.Update(item);

            if (vm.UploadedMedia != null && vm.UploadedMedia.Any())
            {
                await _mediaService.UploadMediaAsyncFromList(
                    vm.UploadedMedia,
                    MediaType.Image,
                    _env.WebRootPath,
                    portfolioItemId: item.ID
                );
            }

            return RedirectToAction("Index");
        }

        // SOFT DELETE
        [HttpPost]
        [Route("admin/portfolioitem/softdelete")]
        public IActionResult SoftDelete([FromBody] Guid id)
        {
            var item = _portfolioService.GetById(id);
            if (item == null) return Json(new { success = false });

            item.Status = Status.Deleted;
            item.UpdatedAt = DateTime.Now;
            _portfolioService.Update(item);

            return Json(new { success = true });
        }

        // SOFT DELETE MEDIA
        [HttpPost]
        [Route("admin/portfolioitem/softdeletemedia")]
        public async Task<IActionResult> SoftDeleteMedia([FromBody] Guid id)
        {
            var result = await _mediaService.SoftDeleteMediaAsync(id);
            return Json(new { success = result });
        }
    }
}
