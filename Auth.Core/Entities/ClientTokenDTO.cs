using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Entities
{
    public class ClientTokenDTO
    {
        public string Token { get; set; }
        public DateTime Expire { get; set; }
    }
}
