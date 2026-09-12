using Main.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using OutboxEmailSender.WorkerService;

var builder = Host.CreateApplicationBuilder(args);

// Automatically hooks into Linux systemd journal logs when running on Linux
//builder.Services.AddSystemd ();

builder.Services.AddWindowsService (options => { options.ServiceName = "Email.WorkerService"; });

// Register your Database Context (Adjust Connection String accordingly)
builder.Services.AddDbContext<LogDbContext> (options =>
    options.UseSqlServer (
        builder.Configuration.GetConnectionString ("LoggingDbConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure (
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds (10),
            errorNumbersToAdd: null
        )
    )
);

// Register the Worker class as a hosted service
builder.Services.AddHostedService<Worker> ();

var host = builder.Build();

host.Run ();
