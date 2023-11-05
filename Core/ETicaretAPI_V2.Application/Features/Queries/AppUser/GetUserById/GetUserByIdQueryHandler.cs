using ETicaretAPI_V2.Application.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using AU = ETicaretAPI_V2.Domain.Entities.Identity;

namespace ETicaretAPI_V2.Application.Features.Queries.AppUser.GetUserById
{
	public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQueryRequest, GetUserByIdQueryResponse>
	{
		readonly UserManager<AU.AppUser> _userManager;

		public GetUserByIdQueryHandler(UserManager<AU.AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
		{
			var data = await _userManager.FindByIdAsync(request.UserId);
			UpdateProfile updateProfile = new()
			{
				Email = data.Email,
				NameSurname = data.NameSurname,
				Username = data.UserName,
			};
			return new()
			{
				UpdateProfile = updateProfile
			};
		}
	}
}
