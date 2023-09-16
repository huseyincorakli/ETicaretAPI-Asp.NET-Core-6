using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.AppUser.GetRolesToUser
{
    public class GetRolesToUserQueryHandler : IRequestHandler<GetRolesToUserQueryRequest, GetRolesToUserQueryResponse>
    {
        readonly IUserService _userService;

        public GetRolesToUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        async Task<GetRolesToUserQueryResponse> IRequestHandler<GetRolesToUserQueryRequest, GetRolesToUserQueryResponse>.Handle(GetRolesToUserQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _userService.GetRolesToUserAsync(request.UserId);
            return new()
            {
                UserRoles = data
            };
        }
    }
}
