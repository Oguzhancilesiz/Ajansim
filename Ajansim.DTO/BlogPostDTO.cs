using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.DTO
{
    public class BlogPostDTO
    {
        public Guid ID { get; set; }

        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }

        public DateTime PublishedAt { get; set; }

        public CategoryDTO   Category { get; set; }

        public List<MediaDTO> MediaFiles { get; set; } = new();
    }
}
