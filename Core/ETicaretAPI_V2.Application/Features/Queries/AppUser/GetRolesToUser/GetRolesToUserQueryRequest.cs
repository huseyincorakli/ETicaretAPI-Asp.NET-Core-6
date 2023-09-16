using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.AppUser.GetRolesToUser
{
    public class GetRolesToUserQueryRequest:IRequest<GetRolesToUserQueryResponse>
    {
        public string UserId { get; set; }
    }
}
