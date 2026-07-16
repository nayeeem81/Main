using Main.Infrastructure;
using Main.Infrastructure.Services;
using Main.Services;
using Main.WebAppCore.Middleware;
using Main.WebAppCore.Tenant;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.Extensions.Options;
using ResourceLibrary.Resources;
using Serilog;
using WebAppCore.Helper;

internal class Program
{
    private static async Task Main (string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Uncomment your host logger integration
        _ = builder.Host.UseSerilog ();

        // Explicitly register Serilog's interface to satisfy concrete infrastructure dependencies
        _ = builder.Services.AddSingleton<Serilog.ILogger> (Serilog.Log.Logger);

        _ = builder.AddSerilogConfiguration ();

        _ = builder.Services.AddScoped<IExceptionLoggingService,ExceptionLoggingService> ();

        _ = builder.Services.AddHttpContextAccessor ();

        _ = builder.Services.AddScoped<ITenantContext,TenantContext> ();

        //  This fixes the framework validation error
        _ = builder.Services.AddTransient<IConfigureOptions<AntiforgeryOptions>
            ,TenantAntiforgeryOptions> ();

        _ = builder.Services.AddScoped<ITenantSetter,TenantSetter> ();

        AppSettings.Current = builder.Configuration.GetSection ("MyAppSettings")
        .Get<MyConfigSettings> () ?? new MyConfigSettings ();

        _ = builder.Services.AddDatabase (builder.Configuration);

        _ = builder.Services.AddDatabaseDeveloperPageExceptionFilter ();

        _ = builder.Services.AddRepository (builder.Configuration);

        _ = builder.Services.AddService (builder.Configuration);

        _ = builder.Services.AddEmailService (builder.Configuration);

        _ = builder.Services.AddCustomLocalization ();

        _ = builder.Services.AddAuthorization (builder.Configuration);

        _ = builder.Services.AddWebOptimizer (pipeline =>
        {
            _ = pipeline.CompileLessFiles ();
        });

        _ = builder.Services.AddControllers (options =>
        {
            options.Filters.Add (new Microsoft.AspNetCore.Mvc.TypeFilterAttribute (typeof (TenantAntiforgeryFilter)));
        });

        _ = builder.Services.AddControllersWithViews ();

        var app = builder.Build();

        if ( app.Environment.IsDevelopment () )
        {
            _ = app.UseMigrationsEndPoint ();
        }
        else
        {
            _ = app.UseExceptionHandler ("/Home/Error");
            _ = app.UseHsts ();
        }

        _ = app.UseGlobalExceptionHandling ();

        _ = app.UseHttpsRedirection ();

        _ = app.UseStatusCodePages ();

        _ = app.UseWebOptimizer ();

        _ = app.UseStaticFiles ();

        _ = app.UseRouting ();

        _ = app.UseSession ();

        _ = app.UseResponseCaching ();

        _ = app.UseCustomLocalization ();

        _ = app.UseMiddleware<TenantResolverHandlingMiddleware> ();

        _ = app.UseCors ();

        _ = app.UseAuthentication ();

        _ = app.UseMiddleware<TenantSecurityMiddleware> ();

        _ = app.UseAuthorization ();

        _ = app.MapControllers ();

        _ = app.MapControllerRoute (name: "MyArea",pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        //  FIX: Create a temporary scope to resolve your Scoped DbContext safely
        using ( var scope = app.Services.CreateScope () )
        {
            var services = scope.ServiceProvider;

            // Pass the scoped service provider, NOT the root container
            await DataSeeder.SeedDataAsync (services);
        }

        await app.RunAsync ();
    }
}