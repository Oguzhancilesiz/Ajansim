using Ajansim.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Core.Abstarcts
{
    public abstract class BaseEntity
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; } /*= new Guid();   */                        // ID (Primary Key)
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Oluşturulma tarihi
        public DateTime? UpdatedAt { get; set; }                // Güncellenme tarihi
        public Status Status { get; set; }
    }
}
