using Main.Model;
using Main.WebAppCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGlobalExceptionHandeler ( );

builder.Services.AddIdentityDbContext ( builder.Configuration );

builder.Services.AddWebBussiessDbContext ( builder.Configuration );






var app = builder.Build();

app.UseExceptionHandler ( );

app.UseStatusCodePages ( );


app.MapControllers ( );


app.Run();
