using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esso.Entity.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
