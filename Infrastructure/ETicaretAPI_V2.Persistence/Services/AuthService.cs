using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Abstraction.Token;
using ETicaretAPI_V2.Application.DTOs;
using ETicaretAPI_V2.Application.Exceptions;
using ETicaretAPI_V2.Application.Helpers;
using ETicaretAPI_V2.Domain.Entities.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using AU = ETicaretAPI_V2.Domain.Entities.Identity;

namespace ETicaretAPI_V2.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IConfiguration _configuration;
        readonly ITokenHandler _tokenHandler;
        readonly SignInManager<AU.AppUser> _signInManager;
        readonly IUserService _userService;
        readonly IMailService _mailService;
        readonly RoleManager<AppRole> _roleManager;

		public AuthService(UserManager<AppUser> userManager, IConfiguration configuration, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService, IMailService mailService, RoleManager<AppRole> roleManager)
		{
			_userManager = userManager;
			_configuration = configuration;
			_tokenHandler = tokenHandler;
			_signInManager = signInManager;
			_userService = userService;
			_mailService = mailService;
			_roleManager = roleManager;
		}

		public async Task<(Token,string Role)> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_Id"] },
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
            AU.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new AU.AppUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,
                        NameSurname = payload.Name,
                        
                        
                    };
                    
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }
            if (result)
            {
                var RoledUser = await _userManager.FindByEmailAsync(payload.Email);
				var role = await _roleManager.FindByNameAsync("Müşteri");
				await _userManager.AddToRoleAsync(RoledUser, role.Name);
				await _userManager.AddLoginAsync(user, info);
            }
            else
            {
                throw new Exception("INVALID EXTERNAL AUTHENTICATION");
            }

            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 9000);
            var role2 = await _userManager.GetRolesAsync(user) ;
            return (token, role2[0]);
        }

        public async Task<(Token,IList<string> Roles,string userId)> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            AU.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(usernameOrEmail);
            }
            if (user == null)
            {
                throw new NotFoundUserException("Hatalı giriş bilgileri!");
            }
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
			
			if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                
                var roles = await _userManager.GetRolesAsync(user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 4500);
                return (token, roles,user.Id);
            }


            throw new AuthenticationErrorException();
        }



        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(user => user.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(4500, user);
               await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration,1500);
                return token;
            }
            else
                throw new NotFoundUserException();
        }


        public async Task PasswordResetAsync(string Email)
        {
            AppUser user = await _userManager.FindByEmailAsync(Email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                resetToken = resetToken.UrlEncode();

                await _mailService.SendPasswordResetMailAsync(Email, user.Id, resetToken);

            }
        }

        public async Task<bool> VerifyResetToken(string resetToken, string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();

                return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);

            }
            return false;
        }
    }
}
