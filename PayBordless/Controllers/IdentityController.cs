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
        return await Login(userInput);

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
   

    [HttpPost]
    public async Task<ActionResult<UserOutputModel>> Login(UserInputModel input)
    {
        var result = await _identity.Login(input);

        if (!result.Succeeded) return BadRequest(result.Errors);

        return new UserOutputModel(result.Data.Token);
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