using ETicaretAPI_V2.Application.DTOs.Address;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Address.AddAddress
{
	public class CreateAddressCommandRequest:IRequest<CreateAddressCommandResponse>
	{
		public string NameSurname { get; set; }
		public string UserId { get; set; }
		public string TelNumber { get; set; }
		public string City { get; set; }
		public string County { get; set; }
		public string AddressInfo { get; set; }
		public string Directions { get; set; }
	}
}
