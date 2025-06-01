using Ajansim.Core.Abstarcts;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }                        // Kategori adı
        [ValidateNever]
        public ICollection<BlogPost>? BlogPosts { get; set; }
    }
}
