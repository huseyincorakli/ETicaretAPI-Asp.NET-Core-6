using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Product.GetProductsByCategory
{
	public class GetProductsByCategoryQueryRequest:IRequest<GetProductsByCategoryQueryResponse>
	{
		public string CategoryId { get; set; }
		public int PageNo { get; set; } = 0;
		public int PageSize { get; set; } = 5;
		public string? ProductName { get; set; }
	}
}
