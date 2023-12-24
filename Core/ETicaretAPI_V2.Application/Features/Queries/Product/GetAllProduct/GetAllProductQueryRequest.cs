using ETicaretAPI_V2.Application.RequestParameters;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
        public float? FirstPriceValue { get; set; }
        public float? SecondPriceValue { get; set; }
        public string? CategoryId { get; set; }
        public int Page { get; set; } = 0;

        public int Size { get; set; } = 5;
		public string? ProductName { get; set; } 
	}
}
