using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Address.GetAddressByUserId
{
	public class GetAddressByUserIdQueryRequest :IRequest<GetAddressByUserIdQueryResponse>
	{
		public string UserId { get; set; }
	}
}
