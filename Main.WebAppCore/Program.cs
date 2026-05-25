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



var app = builder.Build();

app.UseCustomLocalization ( );

app.UseStaticFiles ( );

app.UseRouting ( );

app.UseAuthorization ( );

app.MapDefaultControllerRoute ( );

app.UseExceptionHandler ( );

app.UseStatusCodePages ( );


app.Run();
