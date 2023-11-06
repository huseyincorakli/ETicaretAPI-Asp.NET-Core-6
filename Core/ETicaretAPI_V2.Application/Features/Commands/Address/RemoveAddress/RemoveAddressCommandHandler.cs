using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Address.RemoveAddress
{
	public class RemoveAddressCommandHandler : IRequestHandler<RemoveAddressCommandRequest, RemoveAddressCommandResponse>
	{
		readonly IAddressService _addressService;

		public RemoveAddressCommandHandler(IAddressService addressService)
		{
			_addressService = addressService;
		}

		public async Task<RemoveAddressCommandResponse> Handle(RemoveAddressCommandRequest request, CancellationToken cancellationToken)
		{
			 bool issuceeded= await _addressService.RemoveAdressAsync(request.AddressId);
			return new()
			{
				isSucceeded=issuceeded
			};
		}
	}
}
