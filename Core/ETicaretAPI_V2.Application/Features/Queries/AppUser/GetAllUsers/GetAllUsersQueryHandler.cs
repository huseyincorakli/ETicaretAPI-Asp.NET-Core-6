using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.User;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.AppUser.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GetAllUsersQueryResponse>
    {
        readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            List<ListUser> users =  await _userService.GetAllUsers(request.Page, request.Size,request.searchName);
            int totalUsersCount =  _userService.TotalUserCount;

            return new()
            {
                Users = users,
                TotalUsersCount = totalUsersCount
            };
        }
    }
}
