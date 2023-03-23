using Microsoft.AspNetCore.Mvc;
using PayBordless.Models.Company;
using PayBordless.Services.Company;
using Microsoft.AspNetCore.Authorization;

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
        
        [HttpGet]
        [Authorize]
        public async Task<ICollection<CompanyOutputModel>> GetByUser()
        {
            var userId = _currentUser.UserId;
            var companies = await _companyService.GetAll();
            var userCompanies = companies.Where(c => c.UserId == userId).ToList();
            return userCompanies;
        }

    }
}
