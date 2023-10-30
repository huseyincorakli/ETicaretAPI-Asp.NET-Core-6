using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Category.GetAllCategory
{
	public class GetAllCategoryQueryRequest:IRequest<GetAllCategoryQueryrResponse>
	{
		public int Page { get; set; } = 0;

		public int Size { get; set; } = 5;
	}
}
