using T = ETicaretAPI_V2.Application.DTOs;

namespace ETicaretAPI_V2.Application.Abstraction.Token
{
    public interface ITokenHandler
    {
        //second = life time
        T.Token CreateAccessToken(int second);
    }
}
