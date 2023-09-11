using ETicaretAPI_V2.Application.Abstraction.Services.Authentications;

namespace ETicaretAPI_V2.Application.Abstraction.Services
{
    public interface IAuthService :IExternalAuthentication,IInternalAuthentication
    {
        Task PasswordResetAsync(string Email);
        Task<bool> VerifyResetToken(string resetToken, string userId);
        
    }
}
