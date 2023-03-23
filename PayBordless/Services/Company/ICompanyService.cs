using PayBordless.Models.Company;

namespace PayBordless.Services.Company;

public interface ICompanyService
{
    Task<Result> Upload(CompanyInputModel companyInput, string userId);

    Task<ICollection<CompanyOutputModel>> GetAll();
}