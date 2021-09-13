using Esso.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esso.Entity
{
    [Table("Countries")]
    public class Country : BaseEntity
    {
        public string Name { get; set; }
    }
}
