using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandRequest:IRequest<GoogleLoginCommandResponse>
    {
        public string Id { get; set; }
        public string  IdToken { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Provider { get; set; }
        public string PhotoUrl { get; set; }
        public string  Name { get; set; }

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