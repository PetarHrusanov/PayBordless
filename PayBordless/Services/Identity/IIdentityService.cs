﻿namespace PayBordless.Services.Identity;

using System.Threading.Tasks;
using Data.Models;
using Models.Identity;

public interface IIdentityService
{
    Task<Result<ApplicationUser>> Register(UserInputModel userInput);
    
    Task<ApplicationUser> GetUserById(string userId);
    Task RegisterWithRole(UserWithRoleInputModel userInput);
    Task<Result<UserOutputModel>> Login(UserLoginModel userInput);
    Task CreateRole(string input);
    Task<string[]> GetRoles();
    
    Task<UsersIds[]> GetUsers();
    Task<Result> ChangePassword(string userId, ChangePasswordInputModel changePasswordInput);
}