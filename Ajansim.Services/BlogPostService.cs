using Ajansim.Context;
using Ajansim.Contracts;
using Ajansim.Core.Enums;
using Ajansim.DTO;
using Ajansim.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Services
{
    public class BlogPostService : BaseService<BlogPost>, IBlogPostService
    {
        private readonly AjansimDBContext _dbContext;

        public BlogPostService()
        {
            _dbContext = new AjansimDBContext();
        }
        public List<BlogPostDTO> GetAllDTO()
        {
            return _dbContext.BlogPosts
                .Include(x => x.Category)
                .Include(x => x.MediaFiles)
                .Where(x => x.Status != Status.Deleted)
                .OrderByDescending(x => x.CreatedAt) // 🔥 SON EKLENEN EN ÜSTE
                .Select(x => new BlogPostDTO
                {
                    ID = x.ID,
                    Title = x.Title,
                    Summary = x.Summary,
                    Content = x.Content,
                    PublishedAt = x.PublishedAt,
                    Category = new CategoryDTO
                    {
                        ID = x.Category.ID,
                        Name = x.Category.Name
                    },
                    MediaFiles = x.MediaFiles.Where(x=>x.Status == Status.Active).Select(m => new MediaDTO
                    {
                        ID = m.ID,
                        Url = m.Url,
                        AltText = m.AltText,
                        Extension = m.Extension,
                        MediaType = m.MediaType
                    }).ToList()
                })
                .ToList();

        }

        /// <summary>
        /// ID'ye göre tek BlogPostDTO getirir (Edit veya Details için kullanılabilir).
        /// </summary>
        public BlogPostDTO GetByIdDTO(Guid id)
        {
            var post = _dbContext.BlogPosts
                .Include(x => x.Category)
                .Include(x => x.MediaFiles)
                .FirstOrDefault(x => x.ID == id && x.Status != Status.Deleted);

            if (post == null) return null;

            return new BlogPostDTO
            {
                ID = post.ID,
                Title = post.Title,
                Summary = post.Summary,
                Content = post.Content,
                PublishedAt = post.PublishedAt,
                Category = new CategoryDTO
                {
                    ID = post.Category.ID,
                    Name = post.Category.Name
                },
                MediaFiles = post.MediaFiles.Select(m => new MediaDTO
                {
                    ID = m.ID,
                    Url = m.Url,
                    AltText = m.AltText,
                    Extension = m.Extension,
                    MediaType = m.MediaType
                }).ToList()
            };

        }
    }
}
