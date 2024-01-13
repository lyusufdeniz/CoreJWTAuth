using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public Decimal Price { get; set; }
        public string UserId { get; set; }
    }
}
