using ResourceLibrary.Resources;
using Main.Services;
using WebAppCore.Helper;
using WebAppCore.Runtime.Helper;

internal class Program
{
    private static void Main ( string[] args )
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        AppSettings.Current = builder.Configuration.GetSection ( "MyAppSettings" )
                                     .Get<MyConfigSettings> ( ) ?? new MyConfigSettings ( );

        builder.Services.AddHttpContextAccessor ( );

        builder.Services.AddScoped<IUserContext,UserContext> ( );

        builder.Services.AddServiceDependencies ( builder.Configuration );

        builder.Services.AddCustomLocalization ( );

        builder.Services.AddControllersWithViews ( );

        builder.Services.AddWebOptimizer ( pipeline =>
        {
            pipeline.CompileLessFiles ( );
        } );

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

        app.UseHttpsRedirection ( );

        app.UseStatusCodePages ( );

        app.UseWebOptimizer ( );

        app.UseStaticFiles ( );

        app.UseRouting ( );

        app.UseSession ( );

        app.UseResponseCaching ( );

        app.UseCors ( "AllowAll" );    // for dev: "AllowAll"  // for production: "AllowFrontendApp"   

        app.UseCustomLocalization ( );

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