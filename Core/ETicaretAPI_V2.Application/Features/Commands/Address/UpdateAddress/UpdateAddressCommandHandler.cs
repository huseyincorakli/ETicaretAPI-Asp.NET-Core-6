using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

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
		 bool isSuceeded= 	await _addressService.UpdateAdressAsync(request.AddressId, request.UpdateAddress);
			return new UpdateAddressCommandResponse()
			{
				IsSuceeded = isSuceeded,
			};
		}
	}
}
