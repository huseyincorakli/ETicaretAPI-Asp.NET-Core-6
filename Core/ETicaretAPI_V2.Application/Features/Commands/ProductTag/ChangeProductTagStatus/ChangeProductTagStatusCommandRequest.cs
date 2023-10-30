using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.ProductTag.ChangeProductTagStatus
{
	public class ChangeProductTagStatusCommandRequest:IRequest<ChangeProductTagStatusCommandResponse>
	{
		public string ProductTagId { get; set; }
		public bool IsActive { get; set; }
	}
}
