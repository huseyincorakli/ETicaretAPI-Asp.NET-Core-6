using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using AU = ETicaretAPI_V2.Domain.Entities.Identity;

namespace ETicaretAPI_V2.Application.Features.Commands.AppUser.GoogleLogin
{

    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly IAuthService _authService;
        readonly UserManager<AU.AppUser> _userManager;

		public GoogleLoginCommandHandler(IAuthService authService, UserManager<AU.AppUser> userManager)
		{
			_authService = authService;
			_userManager = userManager;
		}

		public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var (token,role)  = await _authService.GoogleLoginAsync(request.IdToken, 900);
            var user = await _userManager.FindByEmailAsync(request.Email);
            string userId = user.Id;
            return new GoogleLoginCommandResponse()
            {
                Token = token,
                Role = role,
                UserId = userId
            };
        }
    }
}


