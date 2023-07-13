using ETicaretAPI_V2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI_V2.Domain.Entities
{
    public  class Customer:BaseEntitiy
    {
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }  
    }
}
