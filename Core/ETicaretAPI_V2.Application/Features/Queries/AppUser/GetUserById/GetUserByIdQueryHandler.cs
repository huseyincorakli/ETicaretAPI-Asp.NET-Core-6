using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using AU = ETicaretAPI_V2.Domain.Entities.Identity;

namespace ETicaretAPI_V2.Application.Features.Queries.AppUser.GetUserById
{
	public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQueryRequest, GetUserByIdQueryResponse>
	{
		readonly IUserService _userService;

		public GetUserByIdQueryHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
		{
			if (request.UserId==null)
			{
				return new()
				{
					UpdateProfile = null
				};
			}
			else
			{
				var data = await _userService.GetUserById(request.UserId);
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
}
