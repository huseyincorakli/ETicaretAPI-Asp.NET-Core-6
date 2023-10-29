using ETicaretAPI_V2.Application.DTOs.Category;
using MediatR;
using UC = ETicaretAPI_V2.Application.DTOs.Category;

namespace ETicaretAPI_V2.Application.Features.Commands.Category.UpdateCategory
{
	public class UpdateCategoryCommandRequest:IRequest<UpdateCategoryCommandResponse>
	{
		public string CategoryId { get; set; }
		public string CategoryName { get; set; }
		public bool IsActive { get; set; } = true;
	}
}
