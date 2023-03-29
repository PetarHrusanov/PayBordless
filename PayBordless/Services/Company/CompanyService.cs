using Microsoft.EntityFrameworkCore;
using PayBordless.Data;
using PayBordless.Models.Company;
using PayBordless.Models.Service;

namespace PayBordless.Services.Company;

public class CompanyService : ICompanyService
{
    
    private readonly ApplicationDbContext _db;

    public CompanyService(ApplicationDbContext db)
    {
        _db = db;
    }
    
    
    public async Task<Result> Upload(CompanyInputModel companyInput, string userId)
    {
        var newCar = new Data.Models.Company()
        {
            Name = companyInput.Name,
            VAT = companyInput.VAT,
            Owner = companyInput.Owner,
            UserId = userId
        };
        await _db.AddAsync(newCar);
        await _db.SaveChangesAsync();
        
        return Result.Success;
    }

    public async Task<Result> Edit(CompanyOutputModel companyInput)
    {
        var activity = new Data.Models.Company()
        {
            Id = companyInput.Id,
            Name = companyInput.Name,
            VAT = companyInput.VAT,
            Owner = companyInput.Owner,
            UserId = companyInput.UserId,
        };
        _db.Companies.Update(activity);
        await _db.SaveChangesAsync();
        
        return Result.Success;
    }

    public async Task<ICollection<CompanyOutputModel>> GetAll()
        => await _db.Companies.Select(c => new CompanyOutputModel()
        {
            Id = c.Id,
            Name = c.Name,
            Owner = c.Owner,
            UserId = c.UserId,
            VAT = c.VAT
        }).ToListAsync();

    public async Task<ICollection<SerivceOutputModel>> GetAllServices()
        => await _db.Services.Select(c => new SerivceOutputModel()
        {
            Id = c.Id,
            Name = c.Name,
            Price = c.Price,
            CompanyId = c.CompanyId,
        }).ToListAsync();

    public async Task<Result> Delete(int id)
    {
        var company = await _db.Companies.FindAsync(id);

        if (company == null)
        {
            return Result.Failure("Invoice not found.");
        }

        _db.Companies.Remove(company);
        await _db.SaveChangesAsync();

        return Result.Success;
    }
}