using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.AppUser.PasswordReset
{
    public class PasswordResetCommandRequest:IRequest<PasswordResetCommandResponse>
    {
        public string Email { get; set; }
    }
}
