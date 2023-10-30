using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.ProductTag.UpdateProductTag
{
	public class UpdateProductTagCommandRequest:IRequest<UpdateProductTagCommandResponse>
	{
		public string ProductTagId { get; set; }
		public string TagName { get; set; }
		public string CategoryId { get; set; }
		public bool IsActive { get; set; }
	}
}
