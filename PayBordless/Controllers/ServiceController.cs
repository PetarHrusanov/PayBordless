namespace PayBordless.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Models.Service;
using Services.Service;

public class ServiceController : ApiController
{
    private readonly ICurrentUserService _currentUser;
    private readonly IServiceService _serviceService;

    public ServiceController(ICurrentUserService currentUser, IServiceService serviceService)
    {
        _currentUser = currentUser;
        _serviceService = serviceService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Upload(ServiceInputModel inputModel)
    {
        var userId = _currentUser.UserId;
        await _serviceService.Upload(inputModel);
        return Result.Success;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ICollection<SerivceOutputModel>> GetByUser()
    {
        var userId = _currentUser.UserId;
        var services = await _serviceService.GetAll();
        var userCompanies = services.Where(c => c.UserId == userId).ToList();
        return userCompanies;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ICollection<SerivceOutputModel>> GetAll() 
        => await _serviceService.GetAll();
        
    [HttpPut]
    [Authorize]
    public async Task<ActionResult> Edit(ServiceEditModel inputModel)
    {
        var userId = _currentUser.UserId;
        // if (userId != inputModel.UserId) return BadRequest("Incorrect user");
        await _serviceService.Edit(inputModel);
        return Result.Success;
    
    }
    
    
    //
    // [HttpGet("{companyId}/services")]
    // [Authorize]
    // public async Task<ICollection<SerivceOutputModel>> GetServicesById(int companyId)
    // {
    //     var services = await _companyService.GetAllServices();
    //     services = services.Where(c => c.CompanyId == companyId).ToList();
    //     return services;
    // }

}