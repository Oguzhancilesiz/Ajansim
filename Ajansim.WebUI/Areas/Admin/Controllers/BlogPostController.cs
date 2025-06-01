using Ajansim.Contracts;
using Ajansim.Core.Enums;
using Ajansim.Entities;
using Ajansim.Services;
using Ajansim.WebUI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ajansim.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogPostController : Controller
    {
        private readonly IBlogPostService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly IMediaService _mediaService;
        private readonly IWebHostEnvironment _env;

        public BlogPostController(
            IBlogPostService blogService,
            ICategoryService categoryService,
            IMediaService mediaService,
            IWebHostEnvironment env)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _mediaService = mediaService;
            _env = env;
        }

        // ✅ INDEX
        public IActionResult Index()
        {
            var posts = _blogService.GetAll()
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            foreach (var post in posts)
            {
                post.Category = _categoryService.GetById(post.CategoryId);
                post.MediaFiles = _mediaService.GetMediaByEntityAsync(post.ID, "BlogPost").Result;
            }

            return View(posts);
        }

        // ✅ CREATE GET
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new BlogPostViewModel
            {
                CategoryList = _categoryService.GetAll()
                    .Select(x => new SelectListItem
                    {
                        Value = x.ID.ToString(),
                        Text = x.Name
                    }).ToList(),
                PublishedAt = DateTime.Now
            };

            return View(vm);
        }

        // ✅ CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.CategoryList = _categoryService.GetAll()
                    .Select(x => new SelectListItem
                    {
                        Value = x.ID.ToString(),
                        Text = x.Name
                    }).ToList();
                return View(vm);
            }

            var blog = new BlogPost
            {
                ID = Guid.NewGuid(),
                Title = vm.Title,
                Summary = vm.Summary,
                Content = vm.Content,
                CategoryId = vm.CategoryId,
                PublishedAt = vm.PublishedAt,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = Status.Active
            };

            _blogService.Add(blog);

            if (vm.UploadedMedia != null && vm.UploadedMedia.Any())
            {
                await _mediaService.UploadMediaAsyncFromList(
                    vm.UploadedMedia,
                    MediaType.Image,
                    _env.WebRootPath,
                    blogPostId: blog.ID
                );
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blog = _blogService.GetById(id);
            if (blog == null) return NotFound();

            var vm = new BlogPostViewModel
            {
                ID = blog.ID,
                Title = blog.Title,
                Summary = blog.Summary,
                Content = blog.Content,
                PublishedAt = blog.PublishedAt,
                CategoryId = blog.CategoryId,
                CreatedAt = blog.CreatedAt,
                UpdatedAt = blog.UpdatedAt,
                CategoryList = _categoryService.GetAll()
                    .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name })
                    .ToList(),
                MediaFiles = await _mediaService.GetMediaByEntityAsync(blog.ID, "BlogPost")
            };

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogPostViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.CategoryList = _categoryService.GetAll()
                    .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name })
                    .ToList();

                vm.MediaFiles = await _mediaService.GetMediaByEntityAsync(vm.ID, "BlogPost");

                return View(vm);
            }

            var blog = _blogService.GetById(vm.ID);
            if (blog == null) return NotFound();

            blog.Title = vm.Title;
            blog.Summary = vm.Summary;
            blog.Content = vm.Content;
            blog.CategoryId = vm.CategoryId;
            blog.PublishedAt = vm.PublishedAt;
            blog.UpdatedAt = DateTime.Now;

            _blogService.Update(blog);

            if (vm.UploadedMedia != null && vm.UploadedMedia.Any())
            {
                await _mediaService.UploadMediaAsyncFromList(
                    files: vm.UploadedMedia,
                    mediaType: MediaType.Image,
                    webRootPath: _env.WebRootPath,
                    blogPostId: blog.ID
                );
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("admin/blogPost/softdelete/{id}")]
        public IActionResult SoftDelete(Guid id)
        {
            var blog = _blogService.GetById(id);
            if (blog == null)
                return NotFound();

            blog.Status = Status.Deleted;
            blog.UpdatedAt = DateTime.Now;
            _blogService.Update(blog);

            return RedirectToAction("Index");
        }


        [HttpPost]
        [Route("admin/blogPost/softdeletemedia")]
        public async Task<IActionResult> SoftDeleteMedia([FromBody] Guid id)
        {
            var result = await _mediaService.SoftDeleteMediaAsync(id);
            return Json(new { success = result });
        }

    }
}
