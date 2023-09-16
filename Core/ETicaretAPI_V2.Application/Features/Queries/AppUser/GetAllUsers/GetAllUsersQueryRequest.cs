using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.AppUser.GetAllUsers
{
    public class GetAllUsersQueryRequest:IRequest<GetAllUsersQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
