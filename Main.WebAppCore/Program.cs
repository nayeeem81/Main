using Main.Infrastructure;
using Main.Infrastructure.Services;
using Main.Services;
using Main.WebAppCore.Middleware;
using Main.WebAppCore.Tenant;
using ResourceLibrary.Resources;
using Serilog;
using WebAppCore.Helper;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

_ = builder.Services.AddHttpContextAccessor ();

_ = builder.AddSerilogConfiguration ();

_ = builder.Host.UseSerilog ();

_ = builder.Services.AddExceptionLogging ();

_ = builder.Services.AddScoped<ITenantContext,TenantContext> ();

_ = builder.Services.AddScoped<ITenantSetter,TenantSetter> ();

_ = builder.Services.AddScoped<IExceptionLoggingService,ExceptionLoggingService> ();

AppSettings.Current = builder.Configuration.GetSection ("MyAppSettings")
.Get<MyConfigSettings> () ?? new MyConfigSettings ();

_ = builder.Services.ConfigureOptions<TenantAntiforgeryOptions> ();

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

builder.Services.AddControllers (options =>
{
    _ = options.Filters.Add<AntiforgeryActionFilter> ();
});

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

await DataSeeder.SeedDataAsync (app.Services);

await app.RunAsync ();
