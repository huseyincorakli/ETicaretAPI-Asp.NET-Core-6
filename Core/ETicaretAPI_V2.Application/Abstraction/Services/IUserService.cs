using ETicaretAPI_V2.Application.DTOs.User;

namespace ETicaretAPI_V2.Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
    }
}
