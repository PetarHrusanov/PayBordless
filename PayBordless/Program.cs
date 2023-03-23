using PayBordless;
using PayBordless.Data;
using PayBordless.Data.Models;
using PayBordless.Data.Seeding;
using PayBordless.Models.Identity;
using PayBordless.Services.Company;
using PayBordless.Services.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<AdminSettings>(
        builder.Configuration.GetSection(nameof(AdminSettings)),
        config => config.BindNonPublicProperties = true);

builder.Services.Configure<ApplicationSettings>(
    builder.Configuration.GetSection(nameof(ApplicationSettings)),
    config => config.BindNonPublicProperties = true);

builder.Services
    .AddTransient<ITokenGeneratorService, TokenGeneratorService>()
    .AddTransient<IIdentityService, IdentityService>()
    .AddTransient<ICompanyService, CompanyService>()
    ;

builder.Services
    .AddWebService<ApplicationDbContext>(builder.Configuration);

builder.Services
    .AddDefaultIdentity<ApplicationUser>()
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    ;

builder.Services
    .AddTransient<ISeeder, ApplicationDbContextSeeder>();

var app = builder.Build();

app.UseWebService(builder.Environment).Initialize();
app.Run();