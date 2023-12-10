using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.AppUser.GetUserById
{
	public class GetUserByIdQueryRequest:IRequest<GetUserByIdQueryResponse>
	{
		public string? UserId { get; set; }
	}
}
