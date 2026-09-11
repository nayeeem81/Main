using Main.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using OutboxEmailSender.WorkerService;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWindowsService (options => { options.ServiceName = "OutboxEmailSender.WorkerService"; });

// Register your Database Context (Adjust Connection String accordingly)
builder.Services.AddDbContext<LogDbContext> (options =>
    options.UseSqlServer (builder.Configuration.GetConnectionString ("LoggingDbConnection")));

// Register the Worker class as a hosted service
builder.Services.AddHostedService<Worker> ();

var host = builder.Build();

host.Run ();
