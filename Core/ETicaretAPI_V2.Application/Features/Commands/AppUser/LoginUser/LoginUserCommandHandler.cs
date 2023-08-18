using MediatR;
using AU = ETicaretAPI_V2.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using ETicaretAPI_V2.Application.Exceptions;

namespace ETicaretAPI_V2.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<AU.AppUser> _userManager;
        readonly SignInManager<AU.AppUser> _signInManager;

        public LoginUserCommandHandler(UserManager<AU.AppUser> userManager, SignInManager<AU.AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                ///Auhtorize
            }


            return new LoginUserCommandResponse();
        }
    }
}
