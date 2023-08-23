using ETicaretAPI_V2.Application.Abstraction.Token;
using ETicaretAPI_V2.Application.DTOs;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using AU = ETicaretAPI_V2.Domain.Entities.Identity;

namespace ETicaretAPI_V2.Application.Features.Commands.AppUser.GoogleLogin
{
    
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly UserManager<AU.AppUser>_userManager;
        readonly ITokenHandler _tokenHandler;

        public GoogleLoginCommandHandler(UserManager<AU.AppUser> userManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { "744071990792-nf24l0spfdogbfnm0hnurev6tivukkfv.apps.googleusercontent.com" },
            };
            var payload= await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

            var info =  new UserLoginInfo(request.Provider, payload.Subject,request.Provider);
            AU.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user==null)
                {
                    user = new AU.AppUser()
                    {
                        Id=Guid.NewGuid().ToString(),
                        Email=payload.Email,
                        UserName=payload.Email,
                        NameSurname=payload.Name,
                    };
                    var identityResult= await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }
            if (result)
            {
                await _userManager.AddLoginAsync(user, info);
            }
            else
            {
                throw new Exception("INVALID EXTERNAL AUTHENTICATION");
            }

            Token token = _tokenHandler.CreateAccessToken(5);

            return new GoogleLoginCommandResponse()
            {
                Token = token,
            };
            
        }
    }
}


