using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Address.GetAddressById
{
	public class GetAddressByIdQueryRequest:IRequest<GetAddressByIdQueryResponse>
	{
		public string AddressId { get; set; }
	}
}
