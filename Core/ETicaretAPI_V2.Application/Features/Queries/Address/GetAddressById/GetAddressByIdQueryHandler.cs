using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Address.GetAddressById
{
	public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQueryRequest, GetAddressByIdQueryResponse>
	{
		readonly IAddressService _addressService;

		public GetAddressByIdQueryHandler(IAddressService addressService)
		{
			_addressService = addressService;
		}

		public async Task<GetAddressByIdQueryResponse> Handle(GetAddressByIdQueryRequest request, CancellationToken cancellationToken)
		{
			var data=  await _addressService.GetAddressAsync(request.AddressId);

			return new GetAddressByIdQueryResponse()
			{
				Address = data
			};
		}
	}
}
