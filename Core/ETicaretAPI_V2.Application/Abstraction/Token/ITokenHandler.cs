using T = ETicaretAPI_V2.Application.DTOs;

namespace ETicaretAPI_V2.Application.Abstraction.Token
{
    public interface ITokenHandler
    {
        //minute = life time
        T.Token CreateAccessToken(int minute);
    }
}
