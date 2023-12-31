﻿using ETicaretAPI_V2.Application.DTOs.User;
using ETicaretAPI_V2.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI_V2.Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);

        Task<AppUser> GetUserById(string userId);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user,DateTime accessTokenDate,int addedTime);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<List<ListUser>> GetAllUsers(int page,int size, string searchName);
        int TotalUserCount { get; }
        Task AssignRoleToUser(string userId, string[] roles);
        Task<string[]> GetRolesToUserAsync(string userIdOrName);
        Task<bool> HasRolePermissionToEndpointAsync(string name,string code);
        Task<IdentityResult> UpdateProfileAsync(string userId, UpdateProfile user);

	}
}
