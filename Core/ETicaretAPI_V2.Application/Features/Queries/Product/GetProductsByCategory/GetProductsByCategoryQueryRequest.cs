using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Product.GetProductsByCategory
{
	public class GetProductsByCategoryQueryRequest:IRequest<GetProductsByCategoryQueryResponse>
	{
		public string CategoryId { get; set; }
	}
}
