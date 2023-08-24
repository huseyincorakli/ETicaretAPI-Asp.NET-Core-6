using T= ETicaretAPI_V2.Application.DTOs;

namespace ETicaretAPI_V2.Application.Abstraction.Services.Authentications
{
    public interface IExternalAuthentication
    {
        Task<T.Token> GoogleLoginAsync(string idToken,int accessTokenLifeTime);
        
        
        //Task TwitterLoginAsync();
        //Task FacebookLoginAsync();
        //Task TiktokLoginAsync();
    }
}
