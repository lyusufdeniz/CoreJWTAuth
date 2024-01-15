using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Entities
{
    public class Client
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
        //www.myapi1.com , www.myapi2.com gibi clientin erişebileceği apileri tutacagımız list
        public List<string> Audiences { get; set; }
    }
}
