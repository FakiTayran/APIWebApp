﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esso.API
{
    public class AppSettings
    {
        public string JwtSecret { get; set; }
        public string JwtIssuer { get; set; }
    }
}
