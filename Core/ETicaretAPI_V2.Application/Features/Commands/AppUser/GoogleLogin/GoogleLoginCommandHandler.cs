using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.AppUser.GoogleLogin
{

    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public GoogleLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            Token token  = await _authService.GoogleLoginAsync(request.IdToken, 900);
            return new GoogleLoginCommandResponse()
            {
                Token = token
            };
        }
    }
}


