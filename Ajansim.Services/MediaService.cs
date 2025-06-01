using Ajansim.Context;
using Ajansim.Contracts;
using Ajansim.Core.Enums;
using Ajansim.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;

namespace Ajansim.Services
{
    public class MediaService : BaseService<Media>, IMediaService
    {
        private readonly AjansimDBContext _dbContext;

        public MediaService()
        {
            _dbContext = new AjansimDBContext();
        }

        public async Task<bool> DeleteMediaByIdAsync(Guid mediaId, string webRootPath)
        {
            var media = await _dbContext.Medias.FindAsync(mediaId);
            if (media == null) return false;

            var filePath = Path.Combine(webRootPath, media.Url.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (File.Exists(filePath))
                File.Delete(filePath);

            _dbContext.Medias.Remove(media);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task DeleteMediaByEntityAsync(Guid entityId, string entityType)
        {
            var medias = await GetMediaByEntityAsync(entityId, entityType);
            if (medias == null || !medias.Any()) return;

            _dbContext.Medias.RemoveRange(medias);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Media>> GetMediaByEntityAsync(Guid entityId, string entityType)
        {
            return entityType switch
            {
                "BlogPost" => await _dbContext.Medias.Where(m => m.BlogPostId == entityId && m.Status == Status.Active).ToListAsync(),
                "Page" => await _dbContext.Medias.Where(m => m.PageId == entityId && m.Status == Status.Active).ToListAsync(),
                "Service" => await _dbContext.Medias.Where(m => m.ServiceId == entityId && m.Status == Status.Active).ToListAsync(),
                "TeamMember" => await _dbContext.Medias.Where(m => m.TeamMemberId == entityId && m.Status == Status.Active).ToListAsync(),
                "PortfolioItem" => await _dbContext.Medias.Where(m => m.PortfolioItemId == entityId && m.Status == Status.Active).ToListAsync(),
                _ => new List<Media>()
            };
        }

        public async Task<bool> SoftDeleteMediaAsync(Guid mediaId)
        {
            var media = await _dbContext.Medias.FindAsync(mediaId);
            if (media == null) return false;

            media.Status = Status.Deleted;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public Task UpdateMediaAltTextAsync(Guid mediaId, string newAltText)
        {
            throw new NotImplementedException(); // henüz kullanılmıyor
        }

        public async Task UploadMediaAsync(
            IFormFileCollection files,
            MediaType mediaType,
            string webRootPath,
            Guid? blogPostId = null,
            Guid? pageId = null,
            Guid? serviceId = null,
            Guid? teamMemberId = null,
            Guid? portfolioItemId = null)
        {
            if (files == null || !files.Any()) return;

            var folderName = mediaType.ToString().ToLower();
            var uploadPath = Path.Combine(webRootPath, "uploads", folderName);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName);
                var uniqueName = $"{Guid.NewGuid()}{extension}";
                var fullPath = Path.Combine(uploadPath, uniqueName);
                var relativeUrl = Path.Combine("/uploads", folderName, uniqueName).Replace("\\", "/");

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var media = new Media
                {
                    ID = Guid.NewGuid(),
                    FileName = uniqueName,
                    Url = relativeUrl,
                    AltText = Path.GetFileNameWithoutExtension(file.FileName),
                    Extension = extension,
                    MediaType = mediaType,
                    BlogPostId = blogPostId,
                    PageId = pageId,
                    ServiceId = serviceId,
                    TeamMemberId = teamMemberId,
                    PortfolioItemId = portfolioItemId,
                    CreatedAt = DateTime.Now,
                    Status = Status.Active
                };

                _dbContext.Medias.Add(media);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task UploadMediaAsyncFromList(
     List<IFormFile> files,
     MediaType mediaType,
     string webRootPath,
     Guid? blogPostId = null,
     Guid? pageId = null,
     Guid? serviceId = null,
     Guid? teamMemberId = null,
     Guid? portfolioItemId = null)
        {
            if (files == null || !files.Any()) return;

            var folderName = mediaType.ToString().ToLower();
            var uploadPath = Path.Combine(webRootPath, "uploads", folderName);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName);
                var uniqueName = $"{Guid.NewGuid()}{extension}";
                var fullPath = Path.Combine(uploadPath, uniqueName);
                var relativeUrl = Path.Combine("/uploads", folderName, uniqueName).Replace("\\", "/");

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var media = new Media
                {
                    ID = Guid.NewGuid(),
                    FileName = uniqueName,
                    Url = relativeUrl,
                    AltText = Path.GetFileNameWithoutExtension(file.FileName),
                    Extension = extension,
                    MediaType = mediaType,
                    BlogPostId = blogPostId,
                    PageId = pageId,
                    ServiceId = serviceId,
                    TeamMemberId = teamMemberId,
                    PortfolioItemId = portfolioItemId,
                    CreatedAt = DateTime.Now,
                    Status = Status.Active
                };

                _dbContext.Medias.Add(media);
            }

            await _dbContext.SaveChangesAsync();
        }

    }
}
