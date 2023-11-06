using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Address.RemoveAddress
{
	public class RemoveAddressCommandRequest:IRequest<RemoveAddressCommandResponse>
	{
		public string AddressId { get; set; }
	}
}
