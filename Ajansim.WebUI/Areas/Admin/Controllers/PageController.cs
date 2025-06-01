using Ajansim.Contracts;
using Ajansim.Core.Enums;
using Ajansim.Entities;
using Ajansim.WebUI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ajansim.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PageController : Controller
    {
        private readonly IPageService _pageService;
        private readonly IMediaService _mediaService;
        private readonly IWebHostEnvironment _env;

        public PageController(IPageService pageService, IMediaService mediaService, IWebHostEnvironment env)
        {
            _pageService = pageService;
            _mediaService = mediaService;
            _env = env;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            var pages = _pageService.GetAll().ToList();
            var vmList = new List<PageViewModel>();

            foreach (var page in pages)
            {
                var media = await _mediaService.GetMediaByEntityAsync(page.ID, "Page");

                vmList.Add(new PageViewModel
                {
                    ID = page.ID,
                    Title = page.Title,
                    SubTitle = page.SubTitle,
                    Slug = page.Slug,
                    Content = page.Content,
                    MetaDescription = page.MetaDescription,
                    MetaKeyword = page.MetaKeyword,
                    CreatedAt = page.CreatedAt,
                    MediaFiles = media
                });
            }

            return View(vmList);
        }

        // CREATE
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new PageViewModel
            {
                ID = Guid.NewGuid(), // isteğe bağlı
                CreatedAt = DateTime.Now
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PageViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var page = new Page
            {
                ID = Guid.NewGuid(),
                Title = vm.Title,
                SubTitle = vm.SubTitle,
                Slug = vm.Slug,
                Content = vm.Content,
                MetaDescription = vm.MetaDescription,
                MetaKeyword = vm.MetaKeyword,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = Status.Active
            };

            _pageService.Add(page);

            if (vm.UploadedMedia != null && vm.UploadedMedia.Any())
            {
                await _mediaService.UploadMediaAsyncFromList(
                    vm.UploadedMedia,
                    MediaType.Image,
                    _env.WebRootPath,
                    pageId: page.ID
                );
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var page = _pageService.GetById(id);
            if (page == null) return NotFound();

            var media = await _mediaService.GetMediaByEntityAsync(id, "Page");

            var vm = new PageViewModel
            {
                ID = page.ID,
                Title = page.Title,
                SubTitle = page.SubTitle,
                Slug = page.Slug,
                Content = page.Content,
                MetaDescription = page.MetaDescription,
                MetaKeyword = page.MetaKeyword,
                CreatedAt = page.CreatedAt,
                UpdatedAt = page.UpdatedAt,
                MediaFiles = media
            };

            return View(vm); // 👈 Model burada dolu olmalı
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PageViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.MediaFiles = await _mediaService.GetMediaByEntityAsync(vm.ID, "Page");
                return View(vm);
            }

            var page = _pageService.GetById(vm.ID);
            if (page == null) return NotFound();

            page.Title = vm.Title;
            page.SubTitle = vm.SubTitle;
            page.Slug = vm.Slug;
            page.Content = vm.Content;
            page.MetaDescription = vm.MetaDescription;
            page.MetaKeyword = vm.MetaKeyword;
            page.UpdatedAt = DateTime.Now;

            _pageService.Update(page);

            if (vm.UploadedMedia != null && vm.UploadedMedia.Any())
            {
                await _mediaService.UploadMediaAsyncFromList(
                    vm.UploadedMedia,
                    MediaType.Image,
                    _env.WebRootPath,
                    pageId: page.ID
                );
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("admin/page/softdelete/{id}")]
        public IActionResult SoftDelete(Guid id)
        {
            var page = _pageService.GetById(id);
            if (page == null)
                return NotFound();

            page.Status = Status.Deleted;
            page.UpdatedAt = DateTime.Now;
            _pageService.Update(page);

            return RedirectToAction("Index");
        }


        [HttpPost]
        [Route("admin/page/softdeletemedia")]
        public async Task<IActionResult> SoftDeleteMedia([FromBody] Guid id)
        {
            var result = await _mediaService.SoftDeleteMediaAsync(id);
            return Json(new { success = result });
        }

    }

}
