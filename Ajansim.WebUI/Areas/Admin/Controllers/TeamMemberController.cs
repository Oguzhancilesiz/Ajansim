using Ajansim.Contracts;
using Ajansim.Core.Enums;
using Ajansim.Entities;
using Ajansim.WebUI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ajansim.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMemberController : Controller
    {
        private readonly ITeamMemberService _teamMemberService;
        private readonly IMediaService _mediaService;
        private readonly IWebHostEnvironment _env;

        public TeamMemberController(
            ITeamMemberService teamMemberService,
            IMediaService mediaService,
            IWebHostEnvironment env)
        {
            _teamMemberService = teamMemberService;
            _mediaService = mediaService;
            _env = env;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            var members = _teamMemberService.GetAll().ToList();
            var vmList = new List<TeamMemberViewModel>();

            foreach (var member in members)
            {
                var media = await _mediaService.GetMediaByEntityAsync(member.ID, "TeamMember");

                vmList.Add(new TeamMemberViewModel
                {
                    ID = member.ID,
                    FullName = member.FullName,
                    Role = member.Role,
                    LinkedIn = member.LinkedIn,
                    GitHub = member.GitHub,
                    YouTube = member.YouTube,
                    Gmail = member.Gmail,
                    CreatedAt = member.CreatedAt,
                    MediaFiles = media
                });
            }

            return View(vmList);
        }

        // CREATE
        [HttpGet]
        public IActionResult Create()
        {
            return View(new TeamMemberViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamMemberViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var member = new TeamMember
            {
                ID = Guid.NewGuid(),
                FullName = vm.FullName,
                Role = vm.Role,
                Bio = vm.Bio,
                LinkedIn = vm.LinkedIn,
                GitHub = vm.GitHub,
                YouTube = vm.YouTube,
                Gmail = vm.Gmail,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = Status.Active
            };

            _teamMemberService.Add(member);

            if (vm.UploadedMedia != null && vm.UploadedMedia.Any())
            {
                await _mediaService.UploadMediaAsyncFromList(
                    vm.UploadedMedia,
                    MediaType.Image,
                    _env.WebRootPath,
                    teamMemberId: member.ID
                );
            }

            return RedirectToAction("Index");
        }

        // EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var member = _teamMemberService.GetById(id);
            if (member == null) return NotFound();

            var media = await _mediaService.GetMediaByEntityAsync(id, "TeamMember");

            var vm = new TeamMemberViewModel
            {
                ID = member.ID,
                FullName = member.FullName,
                Role = member.Role,
                Bio = member.Bio,
                LinkedIn = member.LinkedIn,
                GitHub = member.GitHub,
                YouTube = member.YouTube,
                Gmail = member.Gmail,
                CreatedAt = member.CreatedAt,
                UpdatedAt = member.UpdatedAt,
                MediaFiles = media
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TeamMemberViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.MediaFiles = await _mediaService.GetMediaByEntityAsync(vm.ID, "TeamMember");
                return View(vm);
            }

            var member = _teamMemberService.GetById(vm.ID);
            if (member == null) return NotFound();

            member.FullName = vm.FullName;
            member.Role = vm.Role;
            member.Bio = vm.Bio;
            member.LinkedIn = vm.LinkedIn;
            member.GitHub = vm.GitHub;
            member.YouTube = vm.YouTube;
            member.Gmail = vm.Gmail;
            member.UpdatedAt = DateTime.Now;

            _teamMemberService.Update(member);

            if (vm.UploadedMedia != null && vm.UploadedMedia.Any())
            {
                await _mediaService.UploadMediaAsyncFromList(
                    vm.UploadedMedia,
                    MediaType.Image,
                    _env.WebRootPath,
                    teamMemberId: member.ID
                );
            }

            return RedirectToAction("Index");
        }

        // SOFT DELETE
        [HttpPost]
        [Route("admin/teammember/softdelete")]
        public IActionResult SoftDelete([FromBody] Guid id)
        {
            var member = _teamMemberService.GetById(id);
            if (member == null) return Json(new { success = false });

            member.Status = Status.Deleted;
            member.UpdatedAt = DateTime.Now;
            _teamMemberService.Update(member);

            return Json(new { success = true });
        }

        // SOFT DELETE MEDIA
        [HttpPost]
        [Route("admin/teammember/softdeletemedia")]
        public async Task<IActionResult> SoftDeleteMedia([FromBody] Guid id)
        {
            var result = await _mediaService.SoftDeleteMediaAsync(id);
            return Json(new { success = result });
        }
    }
}
