using MediatR;
using AU = ETicaretAPI_V2.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using ETicaretAPI_V2.Application.Exceptions;
using ETicaretAPI_V2.Application.Abstraction.Token;
using ETicaretAPI_V2.Application.DTOs;

namespace ETicaretAPI_V2.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<AU.AppUser> _userManager;
        readonly SignInManager<AU.AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(
            UserManager<AU.AppUser> userManager, 
            SignInManager<AU.AppUser> signInManager, 
            ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            AU.AppUser user=  await _userManager.FindByNameAsync(request.UserNameOrEmail);
            if(user==null)
            {
                user=  await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            }
            if (user == null)
            {
                throw new NotFoundUserException("Hatalı giriş bilgileri!");
            }
            SignInResult result =  await _signInManager.CheckPasswordSignInAsync(user, request.Password,false);
            if (result.Succeeded)
            {
                Token token=  _tokenHandler.CreateAccessToken(5);
                return new LoginUserSuccessCommandResponse()
                {
                    Token= token
                };
            }


            throw new AuthenticationErrorException();
        }
    }
}
