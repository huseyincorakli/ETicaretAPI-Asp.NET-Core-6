using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Category.UpdateCategory
{
	public class UpdateCategoryCommandRequest:IRequest<UpdateCategoryCommandResponse>
	{
		public string CategoryId { get; set; }
		public string CategoryName { get; set; }
		public bool IsActive { get; set; } = true;
	}
}
