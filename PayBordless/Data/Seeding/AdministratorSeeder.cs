using PayBordless.Models.Identity;

namespace PayBordless.Data.Seeding;

using System;
using System.Linq;
using System.Threading.Tasks;
    
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Models;

using static  Common.Constants;

public class AdministratorSeeder : ISeeder
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider;
    private readonly AdminSettings _adminSettings;

    public AdministratorSeeder(
        ApplicationDbContext dbContext,
        IServiceProvider serviceProvider,
        IOptions<AdminSettings> adminSettings
        )
    {
        _dbContext = dbContext;
        _serviceProvider = serviceProvider;
        _adminSettings = adminSettings.Value;
    }

    public void SeedAsync()
    {
        if (_dbContext.Users.Any())
        {
            return;
        }
        
        Task
            .Run(async () =>
            {
                var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var user = new ApplicationUser
                {
                    UserName = _adminSettings.AdminUsername,
                    Email = _adminSettings.AdminUsername
                };

                await userManager.CreateAsync(user, _adminSettings.AdminPassword);
                await userManager.AddToRoleAsync(user, AdministratorRoleName);

                await _dbContext.SaveChangesAsync();
            })
            .GetAwaiter()
            .GetResult();
        
    }
}