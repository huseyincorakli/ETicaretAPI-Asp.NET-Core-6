using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandRequest:IRequest<GoogleLoginCommandResponse>
    {
        public string  IdToken { get; set; }
        public string Email { get; set; }
       

    }
}

//email: ""
//firstName: ""
//id: ""
//idToken: ""
//lastName: ""
//name: ""
//photoUrl: ""
//provider: "GOOGLE"