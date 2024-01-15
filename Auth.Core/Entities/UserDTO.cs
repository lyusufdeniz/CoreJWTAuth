using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Entities
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EMail { get; set; }
        public string City { get; set; }
    }
}
