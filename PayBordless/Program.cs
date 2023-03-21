using PayBordless;
using PayBordless.Data;
using PayBordless.Data.Models;
using PayBordless.Services.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddTransient<ITokenGeneratorService, TokenGeneratorService>()
    .AddTransient<IIdentityService, IdentityService>();

builder.Services
    .AddWebService<ApplicationDbContext>(builder.Configuration);

builder.Services
    .AddDefaultIdentity<ApplicationUser>()
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    ;

var app = builder.Build();

app.UseWebService(builder.Environment).Initialize();
app.Run();