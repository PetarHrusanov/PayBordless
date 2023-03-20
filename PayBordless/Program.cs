using PayBordless;
using PayBordless.Data;
using PayBordless.Data.Models;

var builder = WebApplication.CreateBuilder(args);

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