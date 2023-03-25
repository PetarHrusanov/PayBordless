using PayBordless.Models.Service;

namespace PayBordless.Services.Service;

public interface IServiceService
{
    Task<Result> Upload(ServiceInputModel serviceInputModel);
    
    Task<ICollection<SerivceOutputModel>> GetAll();

    Task<Result> Edit(ServiceEditModel editModel);
}