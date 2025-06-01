using Ajansim.Contracts;
using Ajansim.Core.Enums;
using Ajansim.Entities;
using Ajansim.Services;
using Ajansim.WebUI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ajansim.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IService _serviceService;
        private readonly IMediaService _mediaService;
        private readonly IWebHostEnvironment _env;

        public ServiceController(
            IService serviceService,
            IMediaService mediaService,
            IWebHostEnvironment env)
        {
            _serviceService = serviceService;
            _mediaService = mediaService;
            _env = env;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            var services = _serviceService.GetAll().ToList();
            var vmList = new List<ServiceViewModel>();

            foreach (var service in services)
            {
                var media = await _mediaService.GetMediaByEntityAsync(service.ID, "Service");

                vmList.Add(new ServiceViewModel
                {
                    ID = service.ID,
                    Title = service.Title,
                    Description = service.Description,
                    CreatedAt = service.CreatedAt,
                    MediaFiles = media
                });
            }

            return View(vmList);
        }

        // CREATE
        [HttpGet]
        public IActionResult Create()
        {
            return View(new ServiceViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var service = new Service
            {
                ID = Guid.NewGuid(),
                Title = vm.Title,
                Description = vm.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = Status.Active
            };

            _serviceService.Add(service);

            if (vm.UploadedMedia != null && vm.UploadedMedia.Any())
            {
                await _mediaService.UploadMediaAsyncFromList(
                    vm.UploadedMedia,
                    MediaType.Image,
                    _env.WebRootPath,
                    serviceId: service.ID
                );
            }

            return RedirectToAction("Index");
        }

        // EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var service = _serviceService.GetById(id);
            if (service == null) return NotFound();

            var media = await _mediaService.GetMediaByEntityAsync(id, "Service");

            var vm = new ServiceViewModel
            {
                ID = service.ID,
                Title = service.Title,
                Description = service.Description,
                CreatedAt = service.CreatedAt,
                UpdatedAt = service.UpdatedAt,
                MediaFiles = media
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServiceViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.MediaFiles = await _mediaService.GetMediaByEntityAsync(vm.ID, "Service");
                return View(vm);
            }

            var service = _serviceService.GetById(vm.ID);
            if (service == null) return NotFound();

            service.Title = vm.Title;
            service.Description = vm.Description;
            service.UpdatedAt = DateTime.Now;

            _serviceService.Update(service);

            if (vm.UploadedMedia != null && vm.UploadedMedia.Any())
            {
                await _mediaService.UploadMediaAsyncFromList(
                    vm.UploadedMedia,
                    MediaType.Image,
                    _env.WebRootPath,
                    serviceId: service.ID
                );
            }

            return RedirectToAction("Index");
        }

        // SOFT DELETE
        [HttpPost]
        [Route("admin/service/softdelete")]
        public IActionResult SoftDelete([FromBody] Guid id)
        {
            var service = _serviceService.GetById(id);
            if (service == null) return Json(new { success = false });

            service.Status = Status.Deleted;
            service.UpdatedAt = DateTime.Now;
            _serviceService.Update(service);

            return Json(new { success = true });
        }

        // SOFT DELETE MEDIA
        [HttpPost]
        [Route("admin/service/softdeletemedia")]
        public async Task<IActionResult> SoftDeleteMedia([FromBody] Guid id)
        {
            var result = await _mediaService.SoftDeleteMediaAsync(id);
            return Json(new { success = result });
        }
    }
}
