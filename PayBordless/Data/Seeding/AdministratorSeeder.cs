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
    private readonly ApplicationDbContext _db;
    private readonly IServiceProvider _serviceProvider;
    private readonly AdminSettings _adminSettings;

    public AdministratorSeeder(
        ApplicationDbContext db,
        IServiceProvider serviceProvider,
        IOptions<AdminSettings> adminSettings
        )
    {
        _db = db;
        _serviceProvider = serviceProvider;
        _adminSettings = adminSettings.Value;
    }

    public void SeedAsync()
    {
        if (_db.Users.Any())
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

                await _db.SaveChangesAsync();
            })
            .GetAwaiter()
            .GetResult();
        
    }
}