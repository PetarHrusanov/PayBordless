using PayBordless.Models.Company;
using PayBordless.Models.Service;

namespace PayBordless.Services.Company;

public interface ICompanyService
{
    Task<Result> Upload(CompanyInputModel companyInput, string userId);
    Task<Result> Edit(CompanyOutputModel companyInput);
    Task<ICollection<CompanyOutputModel>> GetAll();
    
    Task<ICollection<SerivceOutputModel>> GetAllServices();
    
}