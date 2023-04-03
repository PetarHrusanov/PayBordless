using PayBordless.Models.Identity;

namespace PayBordless.Data.Seeding;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class ApplicationDbContextSeeder : ISeeder
{
    private readonly ApplicationDbContext _db;
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptions<AdminSettings> _adminSettings;

    public ApplicationDbContextSeeder(
        ApplicationDbContext db,
        IServiceProvider serviceProvider,
        IOptions<AdminSettings> adminSettings
        )
    {
        _db = db;
        _serviceProvider = serviceProvider;
        _adminSettings = adminSettings;
    }
        
    public void SeedAsync()
    {
        if (_db == null)
        {
            throw new ArgumentNullException(nameof(_db));
        }

        if (_serviceProvider == null)
        {
            throw new ArgumentNullException(nameof(_serviceProvider));
        }
        

        var logger = (_serviceProvider.GetService<ILoggerFactory>() ?? throw new InvalidOperationException()).CreateLogger(typeof(ApplicationDbContextSeeder));

        var seeders = new List<ISeeder>
        {
            new RolesSeeder(_serviceProvider),
            new AdministratorSeeder(_db, _serviceProvider, _adminSettings),
            new DataSeeder(_serviceProvider)
        };

        foreach (var seeder in seeders)
        {
            Task.Run(async () =>
                {
                    seeder.SeedAsync();
                    await _db.SaveChangesAsync();
                    logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}