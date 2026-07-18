using Main.Infrastructure;
using Main.Services;
using Main.WebAppCore.ActionFilters;
using Main.WebAppCore.CrosscuttingServices;
using Main.WebAppCore.DependentServices;
using Main.WebAppCore.DepententServices;
using Main.WebAppCore.Middleware;
using ResourceLibrary.Resources;
using Serilog;

internal class Program
{
    private static async Task Main (string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Uncomment your host logger integration
        _ = builder.Host.UseSerilog ();

        _ = builder.AddSerilogConfiguration ();

        _ = builder.Services.AddSingleton<Serilog.ILogger> (Serilog.Log.Logger);

        _ = builder.Services.AddExceptionLogging (builder.Configuration);

        _ = builder.Services.AddHttpContextAccessor ();

        _ = builder.Services.AddScoped<ITenantContext,TenantContext> ();

        _ = builder.Services.AddScoped<ITenantSetter,TenantSetter> ();

        _ = builder.Services.AddAntiforgery (options =>
        {
            options.HeaderName = "X-XSRF-TOKEN";
        });

        _ = builder.Services.ConfigureOptions<ConfigureAntiforgeryCookieOptions> ();

        AppSettings.Current = builder.Configuration.GetSection ("MyAppSettings")
        .Get<MyConfigSettings> () ?? new MyConfigSettings ();

        _ = builder.Services.AddDatabase (builder.Configuration);

        _ = builder.Services.AddDatabaseDeveloperPageExceptionFilter ();

        _ = builder.Services.AddRepository (builder.Configuration);

        _ = builder.Services.AddService (builder.Configuration);

        _ = builder.Services.AddSessionMemoryCache (builder.Configuration);

        _ = builder.Services.AddAuthentication (builder.Configuration);

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

        await app.RunAsync ();
    }
}