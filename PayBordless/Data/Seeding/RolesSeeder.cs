﻿namespace PayBordless.Data.Seeding;

using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using Models;

using static  Common.Constants;

internal class RolesSeeder : ISeeder
{
    private readonly IServiceProvider _serviceProvider;

    public RolesSeeder(IServiceProvider serviceProvider) 
        => _serviceProvider = serviceProvider;
    
    public void SeedAsync()
    {
        Task
            .Run(async () =>
            {
                var roleManager = _serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                await SeedRoleAsync(roleManager, AdministratorRoleName);
            })
            .GetAwaiter()
            .GetResult();
    }

    private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            }
        }
    }
}