﻿using AutoMapper;

namespace PayBordless.Services.Identity;

using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Data.Models;
using Models.Identity;

public class IdentityService : IIdentityService
{
    private const string InvalidErrorMessage = "Invalid credentials.";

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ITokenGeneratorService _jwtTokenGenerator;
    private readonly IMapper _mapper;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ITokenGeneratorService jwtTokenGenerator,
        IMapper mapper
        )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _mapper = mapper;
    }

    public async Task<Result<ApplicationUser>> Register(UserInputModel userInput)
    {
        var user = new ApplicationUser
        {
            Email = userInput.Email,
            UserName = userInput.Name
        };

        var identityResult = await _userManager.CreateAsync(user, userInput.Password);

        return identityResult.Succeeded
            ? Result<ApplicationUser>.SuccessWith(user)
            : Result<ApplicationUser>.Failure(identityResult.Errors.Select(e => e.Description));
    }

    public async Task<ApplicationUser> GetUserById(string userId)
        => await _userManager.FindByIdAsync(userId);

    public async Task RegisterWithRole(UserWithRoleInputModel userInput)
    {
        var user = new ApplicationUser
        {
            UserName = userInput.Email,
            Email = userInput.Email
        };

        await _userManager.CreateAsync(user, userInput.Password);
        await _userManager.AddToRoleAsync(user, userInput.Role);
    }

    public async Task<Result<UserOutputModel>> Login(UserLoginModel userInput)
    {
        var user = await _userManager.FindByEmailAsync(userInput.Email);
        if (user == null) return InvalidErrorMessage;

        var passwordValid = await _userManager.CheckPasswordAsync(user, userInput.Password);
        if (!passwordValid) return InvalidErrorMessage;

        var roles = await _userManager.GetRolesAsync(user);

        var token = _jwtTokenGenerator.GenerateToken(user, roles);

        return new UserOutputModel(token, user.UserName);
    }

    public async Task CreateRole(string input)
    {
        if (!await _roleManager.RoleExistsAsync(input))
        { 
            await _roleManager.CreateAsync(new ApplicationRole(input));
        }
    }

    public async Task<string[]> GetRoles() 
        => await _roleManager.Roles.Select(r => r.Name).ToArrayAsync();

    public async Task<UsersIds[]> GetUsers() 
        =>await _mapper.ProjectTo<UsersIds>(_userManager.Users).ToArrayAsync();

    public async Task<Result> ChangePassword(
        string userId,
        ChangePasswordInputModel changePasswordInput)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return InvalidErrorMessage;

        var identityResult = await _userManager.ChangePasswordAsync(
            user,
            changePasswordInput.CurrentPassword,
            changePasswordInput.NewPassword);

        return identityResult.Succeeded
            ? Result.Success
            : Result.Failure(identityResult.Errors.Select(e => e.Description));
    }
}