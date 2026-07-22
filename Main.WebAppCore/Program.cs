using Main.Infrastructure;
using Main.Services;
using Main.WebAppCore.ActionFilters;
using Main.WebAppCore.DependentServices;
using Main.WebAppCore.DepententServices;
using Main.WebAppCore.Middleware;
using Microsoft.AspNetCore.HttpOverrides;
using ResourceLibrary.Resources;
using Serilog;

internal class Program
{
    private static async Task Main (string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // --- 1. Logging & Configuration ---
        _ = builder.Host.UseSerilog ();
        _ = builder.AddSerilogConfiguration ();
        _ = builder.Services.AddSingleton<Serilog.ILogger> (Serilog.Log.Logger);
        _ = builder.Services.AddExceptionLogging (builder.Configuration);

        AppSettings.Current = builder.Configuration.GetSection ("MyAppSettings")
            .Get<MyConfigSettings> () ?? new MyConfigSettings ();

        // --- 2. Core Infrastructure & DI Services ---
        _ = builder.Services.AddHttpContextAccessor ();
        _ = builder.Services.AddScoped<ITenantContext,TenantContext> ();
        _ = builder.Services.AddScoped<ITenantSetter,TenantSetter> ();

        _ = builder.Services.AddDatabase (builder.Configuration);
        _ = builder.Services.AddDatabaseDeveloperPageExceptionFilter ();
        _ = builder.Services.AddRepository (builder.Configuration);
        _ = builder.Services.AddService (builder.Configuration);
        _ = builder.Services.AddSessionMemoryCache (builder.Configuration);
        _ = builder.Services.AddEmailService (builder.Configuration);
        _ = builder.Services.AddCustomLocalization ();

        // --- 3. Antiforgery & Security Setup ---
        _ = builder.Services.AddAntiforgery (options =>
        {
            options.HeaderName = "X-XSRF-TOKEN";
        });

        _ = builder.Services.ConfigureOptions<ConfigureAntiforgeryCookieOptions> ();

        _ = builder.Services.AddAuthorizations (builder.Configuration);
        _ = builder.Services.AddAuthentication (builder.Configuration);

        // --- 4. Web Optimization ---
        _ = builder.Services.AddWebOptimizer (pipeline =>
        {
            _ = pipeline.CompileLessFiles ();
        });

        // --- 5. Unified Controller Routing Registration ---
        _ = builder.Services.AddControllersWithViews (options =>
        {
            // Injecting the dynamic tenant anti-forgery validation filter safely
            //options.Filters.Add (new Microsoft.AspNetCore.Mvc.TypeFilterAttribute (typeof
            //(TenantAntiforgeryFilter)));
        });

        var app = builder.Build();

        // --- 6. HTTP Request Pipeline Execution Order ---
        // CRITICAL FOR NGINX: Translates Nginx reverse-proxy network metadata into 
        if ( app.Environment.IsDevelopment () )
        {
            _ = app.UseMigrationsEndPoint ();
        }
        else
        {
            // _ = app.UseExceptionHandler ("/Home/Error");
            //_ = app.UseHsts ();
        }

        _ = app.UseGlobalExceptionHandling ();
        //_ = app.UseHttpsRedirection ();
        _ = app.UseStatusCodePages ();
        _ = app.UseWebOptimizer ();

        //1.Unpack Nginx headers first (sets PathBase to / tenant1)
        _ = app.UseForwardedHeaders (new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedHost
                               | ForwardedHeaders.XForwardedFor
                               | ForwardedHeaders.XForwardedPrefix
        });

        // CRITICAL: Tenant Resolution must run BEFORE Routing so path-rewriting modifies the route endpoints safely
        _ = app.UseMiddleware<TenantResolverHandlingMiddleware> ();

        _ = app.UseStaticFiles ();

        _ = app.UseRouting ();

        _ = app.UseCors ();

        _ = app.UseSession ();

        _ = app.UseResponseCaching ();

        _ = app.UseCustomLocalization ();

        // --- 7. Authentication & Tenant Authorization Defenses ---
        _ = app.UseAuthentication ();
        //   _ = app.UseAuthorization ();

        // CRITICAL: Runs after Identity sets up User context, allowing you to validate user claims against active tenant contexts
        //  _ = app.UseMiddleware<TenantSecurityMiddleware> ();

        // --- 8. Endpoint Mappings ---
        _ = app.MapControllers ();
        _ = app.MapControllerRoute (
            name: "MyArea",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        await app.RunAsync ();
    }
}