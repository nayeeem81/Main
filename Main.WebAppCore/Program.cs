using Main.Infrastructure;
using Main.Infrastructure.Services;
using Main.Services;
using Main.WebAppCore.Middleware;
using Main.WebAppCore.Tenant;
using ResourceLibrary.Resources;
using WebAppCore.Helper;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

_ = builder.Services.AddHttpContextAccessor ();

_ = builder.Services.AddScoped<ITenantContext,TenantContext> ();

_ = builder.AddSerilogConfiguration ();

_ = builder.Services.ConfigureOptions<TenantAntiforgeryOptions> ();

_ = builder.Services.AddScoped<ITenantSetter,TenantSetter> ();

_ = builder.Services.AddScoped<IExceptionLoggingService,
                           ExceptionLoggingService> ();

//_ = builder.Host.UseSerilog ();

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
    _ = options.Filters.Add<TenantAntiforgeryFilter> ();
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

await DataSeeder.SeedDataAsync (app.Services);

await app.RunAsync ();
