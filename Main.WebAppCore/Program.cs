//using Main.Infrastructure;

    var builder = WebApplication.CreateBuilder(args);

    //builder.Services.AddControllersWithViews ( );

    //builder.Services.AddInfrastructureServices ( builder.Configuration );

    var app = builder.Build();

    app.UseExceptionHandler ( );

    app.UseStatusCodePages ( );

    app.MapControllers ( );

    app.Run();
