using ETicaretAPI_V2.Application.DTOs.Address;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Address.AddAddress
{
	public class CreateAddressCommandRequest:IRequest<CreateAddressCommandResponse>
	{
		public CreateAddress CreateAddress { get; set; }
	}
}
