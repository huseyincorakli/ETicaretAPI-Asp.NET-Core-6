using MediatR;
using UAd = ETicaretAPI_V2.Application.DTOs.Address;

namespace ETicaretAPI_V2.Application.Features.Commands.Address.UpdateAddress
{
	public class UpdateAddressCommandRequest:IRequest<UpdateAddressCommandResponse>
	{
		public string AddressId { get; set; }
		public string NameSurname { get; set; }
		public string TelNumber { get; set; }
		public string City { get; set; }
		public string County { get; set; }
		public string AddressInfo { get; set; }
		public string Directions { get; set; }
	}
}
