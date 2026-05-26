using ResourceLibrary;
using Main.Services;
using WebApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

AppSettings.Current =
     builder.Configuration
            .GetSection ( "MyAppSettings" )
            .Get<MyConfigSettings> ( ) ?? new MyConfigSettings ( );


builder.Services.AddHttpContextAccessor ( );

builder.Services.AddScoped<IUserContext,UserContext> ( );

builder.Services.AddServiceDependencies ( builder.Configuration );

builder.Services.AddCustomLocalization ( );

builder.Services.AddControllersWithViews ( );

builder.Services.AddAuthenticationCore ( );

builder.Services.AddAuthorizationCore ( );

builder.Services.AddWebOptimizer ( pipeline =>
{
    pipeline.CompileLessFiles ( ); 
} );

builder.Logging.ClearProviders ( );

builder.Logging.AddConsole ( );


var app = builder.Build();


if ( app.Environment.IsDevelopment ( ) )
{
    var options = new DeveloperExceptionPageOptions
    {
        SourceCodeLineCount = 10
    };

    app.UseDeveloperExceptionPage ( options );

}
else
{
    app.UseExceptionHandler ( "/Shared/Error" );

    // The default HSTS value is 30 days.
    // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts ( );
}

app.UseStatusCodePages ( );

app.UseHttpsRedirection ( );

app.UseWebOptimizer ( );

app.UseStaticFiles ( );

app.UseRouting ( );

app.UseSession ( );

app.UseResponseCaching ( );

app.UseCors ( );

app.UseCustomLocalization ( );

app.UseAuthentication ( );

app.UseAuthorization ( );

app.MapDefaultControllerRoute ( );

app.Run();
