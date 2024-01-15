﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Entities
{
    public class TokenDTO
    {
        public string AccessToken { get; set; }
        public DateTime AccessExpire { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshExpire { get; set; }
    }
}
