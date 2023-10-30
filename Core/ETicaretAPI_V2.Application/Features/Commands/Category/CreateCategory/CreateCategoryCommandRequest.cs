using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Category.CreateCategory
{
	public class CreateCategoryCommandRequest:IRequest<CreateCategoryCommandResponse>
	{
		public string CategoryName { get; set; }
		public bool IsActive { get; set; }
	}
}
