using Ajansim.Core.Abstarcts;
using Ajansim.Core.Enums;
using Ajansim.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Contracts
{
    public interface IMediaService : IBaseRepository<Media>
    {
        // ✅ Çoklu görsel yükleme (MediaType'a göre klasör, ID bağlama)
        Task UploadMediaAsync(
            IFormFileCollection files,
            MediaType mediaType,
            string webRootPath,
            Guid? blogPostId = null,
            Guid? pageId = null,
            Guid? serviceId = null,
            Guid? teamMemberId = null,
            Guid? portfolioItemId = null);

        // ✅ Belirli bir entity'ye ait tüm medya dosyalarını getir
        Task<List<Media>> GetMediaByEntityAsync(Guid entityId, string entityType);

        // ✅ Belirli bir entity'ye ait tüm medya dosyalarını sil
        Task DeleteMediaByEntityAsync(Guid entityId, string entityType);

        // ✅ Tek medya kaydını sil
        Task<bool> DeleteMediaByIdAsync(Guid mediaId, string webRootPath);

        // (Opsiyonel) ✅ Alt metin gibi alanları güncelle
        Task UpdateMediaAltTextAsync(Guid mediaId, string newAltText);
    }
}
