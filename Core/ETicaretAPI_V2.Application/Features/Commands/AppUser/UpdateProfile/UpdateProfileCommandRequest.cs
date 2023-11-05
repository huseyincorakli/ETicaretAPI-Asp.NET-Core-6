using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.AppUser.UpdateProfile
{
	public class UpdateProfileCommandRequest:IRequest<UpdateProfileCommandResponse>
	{
		public string UserId { get; set; }
		public string NameSurname { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string PasswordConfirm { get; set; }
	}
}
