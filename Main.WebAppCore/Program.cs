using Main.Infrastructure;
using Main.Common;


var builder = WebApplication.CreateBuilder(args);


AppSettings.Current = builder.Configuration
                             .GetSection ( "MyAppSettings" )
                             .Get<MyConfigSettings> ( ) ?? new MyConfigSettings ( );


builder.Services.AddInfrastructureServices ( builder.Configuration );


builder.Services.AddControllersWithViews ( );



var app = builder.Build();

app.UseExceptionHandler ( );

app.UseStatusCodePages ( );

app.MapControllers ( );

app.Run();
