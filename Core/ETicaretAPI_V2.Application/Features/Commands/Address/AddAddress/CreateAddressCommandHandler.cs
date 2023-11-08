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
				
				AddressInfo = request.AddressInfo,
				City = request.City,
				County = request.County,
				Directions = request.Directions,
				NameSurname = request.NameSurname,
				TelNumber = request.TelNumber,
				UserId = request.UserId,
			});

			return new()
			{
				Result = result
			};
		}
	}
}
