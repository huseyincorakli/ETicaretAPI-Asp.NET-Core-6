using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.Address;
using MediatR;
using UA = ETicaretAPI_V2.Application.DTOs.Address;
namespace ETicaretAPI_V2.Application.Features.Commands.Address.UpdateAddress
{
	public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommandRequest, UpdateAddressCommandResponse>
	{
		readonly IAddressService _addressService;

		public UpdateAddressCommandHandler(IAddressService addressService)
		{
			_addressService = addressService;
		}

		public async Task<UpdateAddressCommandResponse> Handle(UpdateAddressCommandRequest request, CancellationToken cancellationToken)
		{
			UA.UpdateAddress updateAddress = new()
			{
				AddressInfo = request.AddressInfo,
				City = request.City,
				County = request.County,
				Directions = request.Directions,
				NameSurname = request.NameSurname,
				TelNumber = request.TelNumber
			};
		 bool isSuceeded= 	await _addressService.UpdateAdressAsync(request.AddressId, updateAddress);
			return new UpdateAddressCommandResponse()
			{
				IsSuceeded = isSuceeded,
			};
		}
	}
}
