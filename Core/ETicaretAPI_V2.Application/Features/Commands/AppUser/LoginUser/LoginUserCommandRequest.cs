using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandRequest:IRequest<LoginUserCommandResponse>
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
