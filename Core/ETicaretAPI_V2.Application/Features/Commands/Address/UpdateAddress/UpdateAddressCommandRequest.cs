using MediatR;
using UAd = ETicaretAPI_V2.Application.DTOs.Address;

namespace ETicaretAPI_V2.Application.Features.Commands.Address.UpdateAddress
{
	public class UpdateAddressCommandRequest:IRequest<UpdateAddressCommandResponse>
	{
		public string AddressId { get; set; }
		public UAd.UpdateAddress UpdateAddress { get; set; }
	}
}
