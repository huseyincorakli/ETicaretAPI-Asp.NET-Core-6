using ETicaretAPI_V2.Application.DTOs.User;
using ETicaretAPI_V2.Domain.Entities.Identity;

namespace ETicaretAPI_V2.Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user,DateTime accessTokenDate,int addedTime);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<List<ListUser>> GetAllUsers(int page,int size);
        int TotalUserCount { get; }
    }
}
