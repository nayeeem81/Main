using Main.Common.Models;
using Main.Infrastructure;
using Main.Services;
using Main.WebAppCore.DependentServices;
using Main.WebAppCore.Middlewares;
using Microsoft.AspNetCore.HttpOverrides;
using ResourceLibrary.Resources;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add this line to register the missing service:
builder.Services.AddSingleton<Microsoft.AspNetCore.Mvc.Infrastructure.IActionContextAccessor,
    Microsoft.AspNetCore.Mvc.Infrastructure.ActionContextAccessor> ();

// Existing configuration...
_ = builder.Services.AddControllersWithViews ();


// Ensure Kestrel hooks into your appsettings.json "Kestrel" section
_ = builder.WebHost.ConfigureKestrel ((context,options) =>
{
    _ = options.Configure (context.Configuration.GetSection ("Kestrel"));
});

// Corrected syntax using .Get<T>()
AppSettings.Current = builder.Configuration.GetSection ("MyAppSettings").Get<ConfigurationSettings> ()!;


_ = builder.Services.AddHttpContextAccessor ();
_ = builder.Services.AddDistributedMemoryCache ();

_ = builder.Services.AddSession (options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes (30);
    options.Cookie.HttpOnly = true;
});

_ = builder.Services.AddScoped<ITenantSetter,ResolvedTenantSetter> ();
_ = builder.Services.AddScoped<IStorageService,LocalStorageService> ();
_ = builder.Services.AddScoped<ITenantAssetResolver,TenantAssetResolver> ();
_ = builder.Services.AddScoped<ITenantCacheService,TenantCacheService> ();

_ = builder.AddSerilogConfiguration ();
_ = builder.Host.UseSerilog ();
_ = builder.Services.AddExceptionLoggingMiddleware (builder.Configuration);

_ = builder.Services.AddDatabase (builder.Configuration);
_ = builder.Services.AddRepository (builder.Configuration);
_ = builder.Services.AddService (builder.Configuration);
_ = builder.Services.AddDatabaseDeveloperPageExceptionFilter ();

_ = builder.Services.AddAntiforgery ();
_ = builder.Services.ConfigureOptions<TenantAntiforgeryOptionMiddleware> ();

_ = builder.Services.AddEmailService (builder.Configuration);
_ = builder.Services.AddCustomLocalization ();
_ = builder.Services.AddAuthorizations (builder.Configuration);
_ = builder.Services.AddAuthentication (builder.Configuration);

_ = builder.Services.AddWebOptimizer (pipeline =>
{
    _ = pipeline.CompileLessFiles ();
});

_ = builder.Services.AddControllersWithViews ();

_ = builder.Services.AddControllers (options =>
{
    options.Filters.Add (new Microsoft.AspNetCore.Mvc.AutoValidateAntiforgeryTokenAttribute ());
});

_ = builder.Services.AddOutputCache ();

var app = builder.Build();

var forwardedHeadersOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders =
            ForwardedHeaders.XForwardedFor |
            ForwardedHeaders.XForwardedHost |
            ForwardedHeaders.XForwardedProto
};

forwardedHeadersOptions.AllowedHosts.Clear ();

forwardedHeadersOptions.KnownNetworks.Clear ();

forwardedHeadersOptions.KnownProxies.Clear ();

_ = app.UseForwardedHeaders (forwardedHeadersOptions);

// Tenancy Context Extraction (Saves TenantId to HttpContext.Items)
_ = app.UseMiddleware<TenantResolverMiddleware> ();

// RUNS SECOND: Reads the resolved tenant from HttpContext.Items and measures performance
app.UseMiddleware<TenantLoggingMiddleware> ();

if ( app.Environment.IsDevelopment () )
{
    _ = app.UseDeveloperExceptionPage ();
    _ = app.UseMigrationsEndPoint ();
}
else
{
    _ = app.UseMiddleware<GlobalExceptionHandlingMiddleware> ();
}

//_ = app.UseMiddleware<GlobalExceptionHandlingMiddleware> ();

// Error handling must sit at the absolute top to catch failures down the line
_ = app.UseStatusCodePages ();
_ = app.UseHttpsRedirection ();

// Global CORS policy (Must be evaluated BEFORE static files and routing)
_ = app.UseCors ();

// Static Assets Optimization Compiler & Handlers (Bypass tenancy/session overhead)
_ = app.UseWebOptimizer ();
_ = app.UseStaticFiles ();

// Multi-Tenant Boundary Identification Routing
_ = app.UseRouting ();

// Session Management Configuration (Tenant-Scoped Setup)
_ = app.UseMiddleware<TenantSessionMiddleware> ();
_ = app.UseSession ();

_ = app.UseCustomLocalization ();

// Step 2: Intercept expired tokens and rotate them
_ = app.UseMiddleware<TokenRefreshMiddleware> ();

// Step 3: Authenticate the user (Unpacks the valid JWT into context.User)
_ = app.UseAuthentication ();

// Step 4: Validate cross-tenant access (Your TenantValidationMiddleware runs safely here!)
_ = app.UseMiddleware<TenantValidationMiddleware> ();

// Step 5: Authorize roles and handle antiforgery
_ = app.UseAuthorization ();

_ = app.UseAntiforgery ();

// 10. Data Storage & Output Optimization Pools
_ = app.UseOutputCache ();

// 11. Endpoint Mappings
_ = app.MapControllers ();

_ = app.MapControllerRoute (
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

_ = app.MapControllerRoute (
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


await app.RunAsync ();
