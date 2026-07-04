using Main.Infrastructure;
using Main.Services;
using Main.WebAppCore.Middleware;
using Main.WebAppCore.Tenant;
using ResourceLibrary.Resources;
using WebAppCore.Helper;
public class Program
{
    private static async Task Main (string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Configure Serilog for global logging
        _ = builder.AddSerilogConfiguration ();

        AppSettings.Current = builder.Configuration.GetSection ("MyAppSettings")
        .Get<MyConfigSettings> () ?? new MyConfigSettings ();

        _ = builder.Services.AddHttpContextAccessor ();
        _ = builder.Services.AddScoped<ITenantContext,TenantContext> ();
        _ = builder.Services.AddScoped<ITenantSetter,TenantSetter> ();
        _ = builder.Services.AddDatabase (builder.Configuration);
        _ = builder.Services.AddDatabaseDeveloperPageExceptionFilter ();
        _ = builder.Services.AddRepository (builder.Configuration);
        _ = builder.Services.AddService (builder.Configuration);
        _ = builder.Services.AddEmailService (builder.Configuration);
        _ = builder.Services.AddCustomLocalization ();
        _ = builder.Services.AddAuthorization (builder.Configuration);
        // 2. Add standard Antiforgery services
        _ = builder.Services.AddAntiforgery ();

        // 3. Register the dynamic, request-scoped configuration hook 
        _ = builder.Services.ConfigureOptions<TenantAntiforgeryConfiguration> ();
        _ = builder.Services.AddWebOptimizer (pipeline => { _ = pipeline.CompileLessFiles (); });

        // Add exception logging service
        _ = builder.Services.AddExceptionLogging ();

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

        // Add global exception handling middleware - should be early in the pipeline
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

        _ = app.UseAntiforgery ();

        _ = app.UseAuthentication ();
        _ = app.UseAuthorization ();

        _ = app.MapControllers ();
        _ = app.MapControllerRoute (name: "MyArea",pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        await DataSeeder.SeedDataAsync (app.Services);
        await app.RunAsync ();
    }
}