using ETicaretAPI_V2.Application.RequestParameters;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
        //public Pagination Pagination { get; set; }
        public int Page { get; set; } = 0;

        public int Size { get; set; } = 5;
    }
}
