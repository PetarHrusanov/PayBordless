using Microsoft.EntityFrameworkCore;
using PayBordless.Data;
using PayBordless.Models.Service;

namespace PayBordless.Services.Service;

public class ServiceService : IServiceService
{
    
    private readonly ApplicationDbContext _db;

    public ServiceService(ApplicationDbContext db)
    {
        _db = db;
    }


    public async Task<Result> Upload(ServiceInputModel serviceInputModel)
    {
        var service = new Data.Models.Service()
        {
            Name = serviceInputModel.Name,
            Price = serviceInputModel.Price,
            CompanyId = serviceInputModel.CompanyId,
        };
        await _db.AddAsync(service);
        await _db.SaveChangesAsync();
        
        return Result.Success;
    }

    public async Task<ICollection<SerivceOutputModel>> GetAll()
        => await _db.Services.Select(s => new SerivceOutputModel()
        {
            Id = s.Id,
            Name = s.Name,
            CompanyName = s.Company.Name,
            CompanyId = s.CompanyId,
            Price = s.Price,
            UserId = s.Company.UserId
        }).ToListAsync();

    public async Task<Result> Edit(ServiceEditModel editModel)
    {
        var activity = new Data.Models.Service()
        {
            Id = editModel.Id,
            Name = editModel.Name,
            Price = editModel.Price,
            CompanyId = editModel.CompanyId,
        };
        _db.Services.Update(activity);
        await _db.SaveChangesAsync();
        
        return Result.Success;
    }
}