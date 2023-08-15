using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI_V2.Application.Features.Queries.GetAllProduct
{
    public class GetAllProductQueryResponse
    {
        public int TotalCount { get; set; } 
        public object Products { get; set; }
    }
}
