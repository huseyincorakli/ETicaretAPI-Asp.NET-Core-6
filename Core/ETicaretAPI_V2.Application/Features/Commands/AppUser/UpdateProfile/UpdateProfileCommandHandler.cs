using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI_V2.Application.Features.Commands.AppUser.UpdateProfile
{
	public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommandRequest, UpdateProfileCommandResponse>
	{
		readonly IUserService _userService;

		public UpdateProfileCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		

		public async Task<UpdateProfileCommandResponse> Handle(UpdateProfileCommandRequest request, CancellationToken cancellationToken)
		{
			var result = await _userService.UpdateProfileAsync(request.UserId, new()
			{
				Email = request.Email,
				NameSurname = request.NameSurname,
				Password = request.Password,
				PasswordConfirm = request.PasswordConfirm,
				Username = request.Username
			});

			return new()
			{
				IsSucceeded = result.Succeeded.ToString()
			};
		}
	}
}
