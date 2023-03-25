using Microsoft.AspNetCore.Mvc;
using PayBordless.Models.Company;
using PayBordless.Services.Company;
using Microsoft.AspNetCore.Authorization;
using PayBordless.Models.Service;

namespace PayBordless.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICurrentUserService _currentUser;
        private readonly ICompanyService _companyService;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ILogger<CompanyController> logger, ICurrentUserService currentUser, ICompanyService companyService)
        {
            _currentUser = currentUser;
            _companyService = companyService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Upload(CompanyInputModel inputModel)
        {
            var userId = _currentUser.UserId;
            await _companyService.Upload(inputModel, userId);
            return Result.Success;
        }
        
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Edit(CompanyOutputModel inputModel)
        {
            var userId = _currentUser.UserId;
            if (userId != inputModel.UserId) return BadRequest("Incorrect user");
            await _companyService.Edit(inputModel);
            return Result.Success;

        }
        
        [HttpGet]
        [Authorize]
        public async Task<ICollection<CompanyOutputModel>> GetAll() 
            => await _companyService.GetAll();

        [HttpGet]
        [Authorize]
        public async Task<ICollection<CompanyOutputModel>> GetByUser()
        {
            var userId = _currentUser.UserId;
            var companies = await _companyService.GetAll();
            var userCompanies = companies.Where(c => c.UserId == userId).ToList();
            return userCompanies;
        }
        
        [HttpGet]
        [Route(Id)]
        [Authorize]
        public async Task<ICollection<SerivceOutputModel>> GetServicesById(int id)
        {
            var services = await _companyService.GetAllServices();
            services = services.Where(c => c.CompanyId == id).ToList();
            return services;
        }

    }
}
