using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.User;
using ETicaretAPI_V2.Application.Exceptions;
using ETicaretAPI_V2.Application.Helpers;
using ETicaretAPI_V2.Application.Repositories.EndpointRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using AU = ETicaretAPI_V2.Domain.Entities.Identity;

namespace ETicaretAPI_V2.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AU.AppUser> _userManager;
        readonly RoleManager<AU.AppRole> _roleManager;
        readonly IEndpointReadRepository _endpointReadRepository;




		public UserService(UserManager<AppUser> userManager, IEndpointReadRepository endpointReadRepository, RoleManager<AppRole> roleManager)
		{
			_userManager = userManager;
			_endpointReadRepository = endpointReadRepository;
			_roleManager = roleManager;
		}

		public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            var id = Guid.NewGuid().ToString();

			IdentityResult result = await _userManager.CreateAsync(new AU.AppUser
            {
                Id = id,
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
                AU.AppUser user=await _userManager.FindByIdAsync(id);
                var role= await _roleManager.FindByNameAsync("Müşteri");
                await _userManager.AddToRoleAsync(user, role.Name);
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

        public async Task<IdentityResult> UpdateProfileAsync(string userId, UpdateProfile user)
        {
            AppUser user1 = await _userManager.FindByIdAsync(userId);
            if (user1 != null)
            {
                var resetToken=  await _userManager.GeneratePasswordResetTokenAsync(user1);
                resetToken=resetToken.UrlEncode();
                if (user.Password != user.PasswordConfirm)
                    throw new PasswordChangeFailedException("Şifreler Uyuşmuyor");

                await UpdatePasswordAsync(userId, resetToken, user.Password);
                user1.Email=user.Email;
                user1.NameSurname = user.NameSurname;
                user1.UserName = user.Username;
				IdentityResult result  = await _userManager.UpdateAsync(user1);
                return result;
            }
            else
            {
                throw new Exception("Böyle bir kullanıcı yok");
            }
        }

        public async Task<List<ListUser>> GetAllUsers(int page, int size)
        {
            var users = await _userManager.Users.
                         Skip(page * size).Take(size).
                         ToListAsync();

            return users.Select(user => new ListUser
            {
                Id = user.Id,
                Email = user.Email,
                NameSurname = user.NameSurname,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName

            }).ToList();
        }

        public int TotalUserCount => _userManager.Users.Count();

        public async Task AssignRoleToUser(string userId, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                await _userManager.AddToRolesAsync(user, roles);
            }
        }

        public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
        {
            AppUser user = await _userManager.FindByIdAsync(userIdOrName);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(userIdOrName);
            }
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return userRoles.ToArray();
            }
            else
            {
                return new[] { "" };
            }
            
        }

        public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
        {
            var userRoles = await GetRolesToUserAsync(name);
            if (!userRoles.Any())
                return false;
            Endpoint? endpoint = await _endpointReadRepository.Table
                                .Include(e => e.Roles)
                                .FirstOrDefaultAsync(e => e.Code == code);

            if (endpoint == null)
                return false;

            var hasRole = false;

            var endpointRoles = endpoint.Roles.Select(r => r.Name);

            foreach (var userRole in userRoles)
            {
                foreach (var endpointRole in endpointRoles)
                {
                    if (userRole== endpointRole)
                    {
                        return true;
                    }
                }
            }
            return false;

        }

		public async Task<AppUser> GetUserById(string userId)
		{
			return await _userManager.FindByIdAsync(userId) ?? null;
		}
	}
}




//foreach (var userRole in userRoles)
//{
//    if (!hasRole)
//    {
//        foreach (var endpointRole in endpointRoles)
//        {
//            if (userRole == endpointRole)
//            {
//                hasRole = true;
//                break;
//            }
//        }
//    }
//    else
//        break;
//}
//return hasRole;