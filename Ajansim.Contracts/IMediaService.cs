using Ajansim.Core.Abstarcts;
using Ajansim.Core.Enums;
using Ajansim.Entities;
using Microsoft.AspNetCore.Http;

namespace Ajansim.Contracts
{
    public interface IMediaService : IBaseRepository<Media>
    {
        Task UploadMediaAsync(
            IFormFileCollection files,
            MediaType mediaType,
            string webRootPath,
            Guid? blogPostId = null,
            Guid? pageId = null,
            Guid? serviceId = null,
            Guid? teamMemberId = null,
            Guid? portfolioItemId = null);

        Task UploadMediaAsyncFromList(
            List<IFormFile> files,
            MediaType mediaType,
            string webRootPath,
            Guid? blogPostId = null,
            Guid? pageId = null,
            Guid? serviceId = null,
            Guid? teamMemberId = null,
            Guid? portfolioItemId = null);

        Task<List<Media>> GetMediaByEntityAsync(Guid entityId, string entityType);

        Task DeleteMediaByEntityAsync(Guid entityId, string entityType);

        Task<bool> DeleteMediaByIdAsync(Guid mediaId, string webRootPath);

        Task<bool> SoftDeleteMediaAsync(Guid mediaId);

        Task UpdateMediaAltTextAsync(Guid mediaId, string newAltText);
    }
}
