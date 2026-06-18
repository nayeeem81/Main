using Main.Infrastructure;
using Main.Services;

using ResourceLibrary.Resources;

using WebAppCore.Helper;

public class Program
{
    private static void Main ( string[] args )
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews ( );

        builder.Services.AddDatabase ( builder.Configuration );

        builder.Services.AddDatabaseDeveloperPageExceptionFilter ( );

        AppSettings.Current = builder.Configuration.GetSection ( "MyAppSettings" )
                                     .Get<MyConfigSettings> ( ) ?? new MyConfigSettings ( );

        builder.Services.AddRepository ( builder.Configuration );

        builder.Services.AddService ( builder.Configuration );

        builder.Services.AddHttpContextAccessor ( );

        builder.Services.AddScoped<IUserContext,WebAppCore.Helper.HttpContextAccessor> ( );

        builder.Services.AddCustomLocalization ( );

        builder.Services.AddControllersWithViews ( );

        builder.Services.AddWebOptimizer ( pipeline => { pipeline.CompileLessFiles ( ); } );

        builder.Logging.ClearProviders ( );

        builder.Logging.AddConsole ( );

        var app = builder.Build();

        if ( app.Environment.IsDevelopment ( ) )
        {
            app.UseMigrationsEndPoint ( );
        }
        else
        {
            app.UseExceptionHandler ( "/Home/Error" );
            app.UseHsts ( );
        }

        //app.UseHttpsRedirection ( );

        app.UseStatusCodePages ( );

        app.UseWebOptimizer ( );

        app.UseStaticFiles ( );

        app.UseRouting ( );

        app.UseSession ( );

        app.UseResponseCaching ( );

        app.UseCors ( );

        app.UseCustomLocalization ( );

        app.UseMiddleware<TenantResolverMiddleware> ( );

        app.UseAuthentication ( );

        app.UseAuthorization ( );

        app.MapControllers ( );

        app.MapControllerRoute (
            name: "MyArea",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}" );


        app.MapControllerRoute (
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}" );

        app.Run ( );
    }
}