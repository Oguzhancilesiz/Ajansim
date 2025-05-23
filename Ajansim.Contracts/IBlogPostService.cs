using Ajansim.Core.Abstarcts;
using Ajansim.DTO;
using Ajansim.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Contracts
{
    public interface IBlogPostService : IBaseRepository<BlogPost>
    {
        List<BlogPostDTO> GetAllDTO();
        BlogPostDTO GetByIdDTO(Guid id);
    }
}
