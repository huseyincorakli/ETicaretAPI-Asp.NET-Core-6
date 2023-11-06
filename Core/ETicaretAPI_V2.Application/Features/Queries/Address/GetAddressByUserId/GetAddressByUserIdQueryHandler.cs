using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Address.GetAddressByUserId
{
	public class GetAddressByUserIdQueryHandler : IRequestHandler<GetAddressByUserIdQueryRequest, GetAddressByUserIdQueryResponse>
	{
		readonly IAddressService _addressService;

		public GetAddressByUserIdQueryHandler(IAddressService addressService)
		{
			_addressService = addressService;
		}

		public async Task<GetAddressByUserIdQueryResponse> Handle(GetAddressByUserIdQueryRequest request, CancellationToken cancellationToken)
		{
			var data = await _addressService.GetAddressByUserIDAsync(request.UserId);
			return new()
			{
				Address = data
			};
		}
	}
}
