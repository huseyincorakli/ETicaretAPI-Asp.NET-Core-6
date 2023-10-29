using MediatR;
using CC = ETicaretAPI_V2.Application.DTOs.Category;

namespace ETicaretAPI_V2.Application.Features.Commands.Category.CreateCategory
{
	public class CreateCategoryCommandRequest:IRequest<CreateCategoryCommandResponse>
	{
		public CC.CreateCategory CreateCategory { get; set; }
	}
}
