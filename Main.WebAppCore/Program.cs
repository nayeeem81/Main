using Main.Infrastructure;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices ( builder.Configuration );

builder.Services.AddIdentity<IdentityUser,IdentityRole> ( options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes ( 5 );
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.User.RequireUniqueEmail = true;
} );


var app = builder.Build();

app.UseExceptionHandler ( );

app.UseStatusCodePages ( );

app.MapControllers ( );

app.Run();
