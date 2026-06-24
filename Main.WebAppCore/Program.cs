using Main.Infrastructure;
using Main.Services;

using Microsoft.AspNetCore.Authorization;

using ResourceLibrary.Resources;

using WebAppCore.Helper;
public class Program
{
    private static async Task Main (string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        _ = builder.Services.AddHttpContextAccessor ();
        _ = builder.Services.AddTransient<IAuthorizationHandler,TenantRoleHandler> ();
        _ = builder.Services.AddScoped<IUserContext,UserContext> ();
        _ = builder.Services.AddScoped<ITenantSetter,TenantSetter> ();
        _ = builder.Services.AddDatabase (builder.Configuration);
        AppSettings.Current = builder.Configuration.GetSection ("MyAppSettings")
                              .Get<MyConfigSettings> () ?? new MyConfigSettings ();
        _ = builder.Services.AddDatabaseDeveloperPageExceptionFilter ();
        _ = builder.Services.AddRepository (builder.Configuration);
        _ = builder.Services.AddService (builder.Configuration);
        _ = builder.Services.AddCustomLocalization ();
        _ = builder.Services.AddControllersWithViews ();
        _ = builder.Services.AddWebOptimizer (pipeline => { _ = pipeline.CompileLessFiles (); });
        _ = builder.Logging.ClearProviders ();
        _ = builder.Logging.AddConsole ();

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

        _ = app.UseHttpsRedirection ();
        _ = app.UseStatusCodePages ();
        _ = app.UseWebOptimizer ();
        _ = app.UseStaticFiles ();
        _ = app.UseRouting ();
        _ = app.UseSession ();
        _ = app.UseResponseCaching ();
        _ = app.UseCors ();
        _ = app.UseCustomLocalization ();
        _ = app.UseMiddleware<TenantResolverMiddleware> ();
        _ = app.UseAuthentication ();
        _ = app.UseAuthorization ();
        _ = app.MapControllers ();
        _ = app.MapControllerRoute (name: "MyArea",pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
        await DataSeeder.SeedDataAsync (app.Services);
        await app.RunAsync ();
    }
}