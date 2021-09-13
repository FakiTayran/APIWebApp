using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esso.Entity.Models
{
    [Table("Cities")]
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public int CountryID { get; set; }
    }
}
