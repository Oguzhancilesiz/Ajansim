using Ajansim.Contracts;
using Ajansim.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Ajansim.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MediaController : Controller
    {
        private readonly IMediaService _mediaService;
        private readonly IWebHostEnvironment _env;

        public MediaController(IMediaService mediaService, IWebHostEnvironment env)
        {
            _mediaService = mediaService;
            _env = env;
        }

        public IActionResult Index()
        {
            var mediaList = _mediaService.GetAll().ToList();
            return View(mediaList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFileCollection files, MediaType mediaType, Guid? blogPostId = null)
        {
            if (files == null || files.Count == 0)
            {
                ModelState.AddModelError("", "Dosya seçilmedi.");
                return View();
            }

            await _mediaService.UploadMediaAsync(files, mediaType, _env.WebRootPath, blogPostId: blogPostId);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            var media = _mediaService.GetById(id);
            if (media == null)
                return NotFound();

            _mediaService.Delete(media);
            return RedirectToAction("Index");
        }
    }
}
