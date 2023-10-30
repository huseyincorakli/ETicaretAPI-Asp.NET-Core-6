using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Category.GetCategoryById
{
	public class GetCategoryByIdQueryRequest:IRequest<GetCategoryByIdQueryResponse>
	{
		public string CategoryId { get; set; }
	}
}
