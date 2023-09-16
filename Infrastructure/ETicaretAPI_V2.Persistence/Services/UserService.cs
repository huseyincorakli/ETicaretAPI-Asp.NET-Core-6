using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.User;
using ETicaretAPI_V2.Application.Exceptions;
using ETicaretAPI_V2.Application.Helpers;
using ETicaretAPI_V2.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AU = ETicaretAPI_V2.Domain.Entities.Identity;

namespace ETicaretAPI_V2.Persistence.Services
{
    public class UserService : IUserService
    {
        UserManager<AU.AppUser> _userManager;

        

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new AU.AppUser
            {
                Id = Guid.NewGuid().ToString(),
                NameSurname = model.NameSurname,
                Email = model.Email,
                UserName = model.Username,

            }, model.Password);
            CreateUserResponse response = new CreateUserResponse
            {
                Succeeded = result.Succeeded,
            };
            if (result.Succeeded)
            {
                response.Message = "Kullanıcı kaydı başarılı";
            }
            else
            {
                foreach (var errors in result.Errors)
                {
                    response.Message += $"{errors.Code} - {errors.Description} \n ";
                }
            }

            return response;

        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addedTime)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addedTime);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                }
                else
                    throw new PasswordChangeFailedException();

            }
        }

        public async Task<List<ListUser>> GetAllUsers(int page, int size)
        {
            var users = await _userManager.Users.
                         Skip(page * size).Take(size).
                         ToListAsync();

            return users.Select(user => new ListUser
            {
               Id=user.Id,
               Email=user.Email,
               NameSurname=user.NameSurname,
               TwoFactorEnabled=user.TwoFactorEnabled,
               UserName = user.UserName

            }).ToList();
        }

        public int TotalUserCount => _userManager.Users.Count();
    }
}
