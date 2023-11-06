using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Address.AddAddress
{
	public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommandRequest, CreateAddressCommandResponse>
	{
		readonly IAddressService _addressService;

		public CreateAddressCommandHandler(IAddressService addressService)
		{
			_addressService = addressService;
		}

		public async Task<CreateAddressCommandResponse> Handle(CreateAddressCommandRequest request, CancellationToken cancellationToken)
		{
		 	var result= await _addressService.AddAdressAsync(new()
			{
				
				AddressInfo = request.CreateAddress.AddressInfo,
				City = request.CreateAddress.City,
				County = request.CreateAddress.County,
				Directions = request.CreateAddress.Directions,
				NameSurname = request.CreateAddress.NameSurname,
				TelNumber = request.CreateAddress.TelNumber,
				UserId = request.CreateAddress.UserId,
			});

			return new()
			{
				Result = result
			};
		}
	}
}
