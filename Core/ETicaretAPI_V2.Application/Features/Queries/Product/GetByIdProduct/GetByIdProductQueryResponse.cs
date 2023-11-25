using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI_V2.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryResponse
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public string  Description { get; set; }
        public string CategoryName { get; set; }
        public object ImageFiles { get; set; }
    }
}
