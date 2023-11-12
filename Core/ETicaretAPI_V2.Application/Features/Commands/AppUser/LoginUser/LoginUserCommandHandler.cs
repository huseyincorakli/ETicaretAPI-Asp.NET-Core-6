using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
           var data =  await _authService.LoginAsync(request.UserNameOrEmail, request.Password, 9000);
            return new LoginUserSuccessCommandResponse()
            {
                Token = data.Item1,
                Role = data.Roles[0],
                UserId=data.userId
            };
        }
    }
}
