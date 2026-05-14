using Main.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace Main.WebAppCore;

public static class IdentityExtension
{
    public static void AddIdentityDbContext ( this IServiceCollection services, IConfiguration configuration )
    {
       
        services.AddIdentityServices ( configuration );

        services.AddIdentityCore<IdentityUser> ( options =>
                 options.IdentityGlobalOptions ( ))
                        .AddEntityFrameworkStores<ApplicationDbContext> ( );

        services.AddIdentityCore<IdentityRole> ( )
                .AddEntityFrameworkStores<ApplicationDbContext> ( );
    }

    public static void IdentityGlobalOptions ( this IdentityOptions options )
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
    }

    public static void AddGlobalExceptionHandeler ( this IServiceCollection services )
    {
        services.AddControllers ( );
        services.AddControllersWithViews ( );
        services.AddProblemDetails ( );
        services.AddExceptionHandler<GlobalExceptionHandler> ( );
    }
}

    
public class GlobalExceptionHandler: IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler ( ILogger<GlobalExceptionHandler> logger )
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync (
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken )
    {

        _logger.LogError ( exception,"An unhandled exception occurred: {Message}",exception.Message );

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred",
            Detail = exception.Message 
        };

        
        if ( exception is ArgumentException )
        {
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Title = "Validation Error";
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        
        await httpContext.Response.WriteAsJsonAsync ( problemDetails,cancellationToken );

        
        return true;
    }
}