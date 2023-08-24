using ETicaretAPI_V2.Application.DTOs.User;
using ETicaretAPI_V2.Domain.Entities.Identity;

namespace ETicaretAPI_V2.Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task UpdateRefreshToken(string refreshToken, AppUser user,DateTime accessTokenDate,int addedTime);
        
    }
}
