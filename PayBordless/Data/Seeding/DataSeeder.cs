using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PayBordless.Data.Models;

namespace PayBordless.Data.Seeding;

internal class DataSeeder : ISeeder
{
    private readonly IServiceProvider _serviceProvider;

    public DataSeeder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async void SeedAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var user = await userManager.FindByEmailAsync("mojo_jojo@abv.bg");
        if (user == null)
        {
            throw new Exception("User with email mojo_jojo@abv.bg not found.");
        }

        if (!db.Companies.Any())
        {
            db.Companies.AddRange(GetCompanies(user.Id));
            await db.SaveChangesAsync();
        }
        
        var firstCompanyId = await db.Companies.Where(c => c.Name == "Company 1").FirstOrDefaultAsync();
        var secondCompanyId = await db.Companies.Where(c => c.Name == "Company 2").FirstOrDefaultAsync();
        var thirdCompanyId = await db.Companies.Where(c => c.Name == "Company 3").FirstOrDefaultAsync();

        if (!db.Services.Any())
        {
            db.Services.AddRange(GetServices(firstCompanyId.Id));
            db.Services.AddRange(GetServices(secondCompanyId.Id));
            db.Services.AddRange(GetServices(thirdCompanyId.Id));
            
            await db.SaveChangesAsync();
        }

        var firstServiceId = await db.Services.Where(c => c.Company.Name == "Company 2" && c.Name == "Service 1")
            .FirstOrDefaultAsync();
        
        if (!db.Invoices.Any())
        {
            db.Invoices.AddRange(GetInvoices(firstServiceId.Id, firstCompanyId.Id, user.Id));
            await db.SaveChangesAsync();
        }
    }
    
    private static IEnumerable<Company> GetCompanies(string userId) =>
        new List<Company>
        {
            new() {Name = "Company 1",
                VAT = 123456789,
                Owner = "Owner 1",
                UserId =userId},
            new() {Name = "Company 2",
                VAT = 5678,
                Owner = "Owner 3",
                UserId =userId},
            new() {Name = "Company 3",
                VAT = 5678,
                Owner = "Owner 3",
                UserId =userId}
        };
    
    private static IEnumerable<Service> GetServices(int companyId) =>
        new List<Service>
        {
            new() {
                Name = "Service 1",
                Price = 120,
                CompanyId = companyId
            },
            new() {
                Name = "Service 2",
                Price = 121,
                CompanyId = companyId
            },
            new() {
                Name = "Service 3",
                Price = 125,
                CompanyId = companyId
            },
        };
    
    private static IEnumerable<Invoice> GetInvoices(int serviceId, int recipientId, string userId) =>
        new List<Invoice>
        {
            new() {
                ServiceId = serviceId,
                Quantity = 1,
                Total = 200,
                RecipientId = recipientId,
                Approved = true,
                UserId = userId
            },
            new() {
                ServiceId = serviceId,
                Quantity = 1,
                Total = 200,
                RecipientId = recipientId,
                Approved = true,
                UserId = userId
            },
            new() {
                ServiceId = serviceId,
                Quantity = 1,
                Total = 200,
                RecipientId = recipientId,
                UserId = userId
            },
        };
}