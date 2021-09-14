using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esso.Entity.Models.Dtos
{
    public class TokenResult
    {
        public string access_token { get; set; }
        public string token_type => "Bearer";
        public int expires_in { get; set; }
    }
}
