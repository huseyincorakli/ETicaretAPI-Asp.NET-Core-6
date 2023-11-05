using ETicaretAPI_V2.Application.DTOs;

namespace ETicaretAPI_V2.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandResponse
    {
        public Token Token { get; set; }
        public string Role { get; set; }
        public string UserId { get; set; }
    }
}
