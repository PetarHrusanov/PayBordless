using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayBordless.Models.Identity;
using PayBordless.Services.Identity;

namespace PayBordless.Controllers;

public class IdentityController : ApiController
{
    private readonly IIdentityService _identity;
    private readonly ICurrentUserService _currentUser;

    public IdentityController(IIdentityService identity, ICurrentUserService currentUser)
    {
        _identity = identity;
        _currentUser = currentUser;
    }

    [HttpPost]
    public async  Task<ActionResult<UserOutputModel>> Register([FromBody]UserInputModel userInput)
    {
        
        await _identity.Register(userInput);
        var loginModel = new UserLoginModel
        {
            Email = userInput.Email,
            Password = userInput.Password
        };
        return await Login(loginModel);

    }
    
    [HttpPost]
    public async Task RegisterUserWithRole(UserWithRoleInputModel userWithRoleInputModel)
    {
        await _identity.RegisterWithRole(userWithRoleInputModel);
        
        // if (!result.Succeeded) return BadRequest(result.Errors);
    }
    
    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task CreateRole([FromForm]string role)
    {
        await _identity.CreateRole(role);
        // if (!result.Succeeded) return BadRequest(result.Errors);

    }
    
    [HttpGet]
    public async Task<string[] > GetRoles()
        => await _identity.GetRoles();

    [HttpGet]
    public async Task<UsersIds[] > GetUsers() 
        => await _identity.GetUsers();
    
    [HttpGet]
    public async  Task<ActionResult<UsernameModel>> Username()
    {
        var userId = _currentUser.UserId;
        var user = await _identity.GetUserById(userId);
        var userName = new UsernameModel()
        {
            Name = user.UserName
        };
        return userName;
    }
   

    [HttpPost]
    public async Task<ActionResult<UserOutputModel>> Login(UserLoginModel input)
    {
        var result = await _identity.Login(input);

        if (!result.Succeeded) return BadRequest(result.Errors);
 
        var kur = new UserOutputModel(result.Data.Token, result.Data.User);

        return kur;
    }

    [HttpPut]
    [Authorize]
    [Route(nameof(ChangePassword))]
    public async Task<ActionResult> ChangePassword(ChangePasswordInputModel input)
    {
        return await _identity.ChangePassword(_currentUser.UserId, new ChangePasswordInputModel
        {
            CurrentPassword = input.CurrentPassword,
            NewPassword = input.NewPassword
        });
    }

}