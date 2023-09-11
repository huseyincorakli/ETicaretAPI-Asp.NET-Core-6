using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Exceptions;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.AppUser.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
    {
        readonly IUserService _userService;

        public UpdatePasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            if (!request.Password.Equals(request.PasswordConfirm))
                throw new PasswordChangeFailedException("Girdiğiniz şifre değerleri uyuşmuyor");
            await _userService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password);

            return new();
        }
    }
}
