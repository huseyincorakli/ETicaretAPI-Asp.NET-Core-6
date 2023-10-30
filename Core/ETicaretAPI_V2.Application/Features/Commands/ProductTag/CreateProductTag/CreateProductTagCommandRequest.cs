using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.ProductTag.CreateProductTag
{
	public class CreateProductTagCommandRequest:IRequest<CreateProductTagCommandResponse>
	{
		public string ProductTagName { get; set; }
		public string CategoryId { get; set; }
		public bool IsActive { get; set; }
	}
}
