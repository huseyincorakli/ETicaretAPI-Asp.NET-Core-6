using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.ProductTag.GetAllProductTag
{
	public class GetAllProductTagQueryRequest:IRequest<GetAllProductTagQueryResponse>
	{
		public int Page { get; set; } = 0;

		public int Size { get; set; } = 5;
	}
}
