using Ajansim.Contracts;
using Ajansim.Core.Enums;
using Ajansim.DTO;
using Ajansim.Entities;
using Ajansim.Services;
using Ajansim.WebUI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ajansim.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogPostController : Controller
    {
        private readonly IBlogPostService _blogPostService;
        private readonly ICategoryService _categoryService;
        private readonly IMediaService _mediaService;
        private readonly IWebHostEnvironment _env;

        public BlogPostController(
            IBlogPostService blogPostService,
            ICategoryService categoryService,
            IMediaService mediaService,
            IWebHostEnvironment env)
        {
            _blogPostService = blogPostService;
            _categoryService = categoryService;
            _mediaService = mediaService;
            _env = env;
        }

        // ✅ DTO kullanarak listeleme
        public IActionResult Index()
        {
            var blogs = _blogPostService.GetAllDTO();
            return View(blogs);
        }

        // ✅ Yeni oluşturma
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = GetCategoryList();
            return View(new BlogPost());
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = GetCategoryList();
                return View(blogPost);
            }

            blogPost.ID = Guid.NewGuid();
            blogPost.Status = Status.Active;
            blogPost.CreatedAt = DateTime.Now;
            blogPost.UpdatedAt = DateTime.Now;
            blogPost.PublishedAt = DateTime.Now;

            _blogPostService.Add(blogPost);

            if (Request.Form.Files.Count > 0)
            {
                await _mediaService.UploadMediaAsync(
                    files: Request.Form.Files,
                    mediaType: MediaType.Image,
                    webRootPath: _env.WebRootPath,
                    blogPostId: blogPost.ID
                );
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blog = _blogPostService.GetById(id);
            if (blog == null) return NotFound();

            ViewBag.Categories = GetCategoryList();
            ViewBag.MediaFiles = await _mediaService.GetMediaByEntityAsync(id, "BlogPost");

            return View(blog);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = GetCategoryList();
                ViewBag.MediaFiles = await _mediaService.GetMediaByEntityAsync(blogPost.ID, "BlogPost");
                return View(blogPost);
            }

            blogPost.UpdatedAt = DateTime.Now;
            _blogPostService.Update(blogPost);

            if (Request.Form.Files.Count > 0)
            {
                await _mediaService.UploadMediaAsync(
                    files: Request.Form.Files,
                    mediaType: MediaType.Image,
                    webRootPath: _env.WebRootPath,
                    blogPostId: blogPost.ID
                );
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMedia(Guid mediaId)
        {
            var result = await _mediaService.DeleteMediaByIdAsync(mediaId, _env.WebRootPath);
            return Json(new { success = result });
        }


        // ✅ Soft Delete
        public IActionResult Delete(Guid id)
        {
            var blog = _blogPostService.GetById(id);
            if (blog == null) return NotFound();

            blog.Status = Status.Deleted;
            blog.UpdatedAt = DateTime.Now;
            _blogPostService.Update(blog);

            return RedirectToAction("Index");
        }

        // 📌 Ortak kategori listesi
        private List<SelectListItem> GetCategoryList()
        {
            return _categoryService.GetAll()
                .Select(x => new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = x.Name
                }).ToList();
        }
    }
}
