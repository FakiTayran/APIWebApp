using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esso.Entity.Models.Dtos
{
    public class GeneralResponse<T>
    {
        public T Result { get; set; }
        public int Code { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
