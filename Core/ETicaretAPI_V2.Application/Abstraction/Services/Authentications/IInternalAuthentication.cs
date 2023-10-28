using T = ETicaretAPI_V2.Application.DTOs;
namespace ETicaretAPI_V2.Application.Abstraction.Services.Authentications
{
    public interface IInternalAuthentication
    {
        Task<(T.Token, IList<string> Roles)> LoginAsync(string usernameOrEmail,string password,int accessTokenLifetime);
        Task<T.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
