using Main.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args); // Existing template line

// --- ADD THIS BLOCK ---
// 1. Fetch the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("LoggingDbConnection");

// 2. Register your Logging DbContext (Assuming SqlServer, change to UseNpgsql or UseMySql if needed)
builder.Services.AddDbContext<LogDbContext> (options =>
    options.UseSqlServer (connectionString));
// ----------------------


// Add services to the container.

builder.Services.AddControllers ();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment () )
{
    _ = app.UseSwagger ();
    _ = app.UseSwaggerUI ();
}

app.UseAuthorization ();

app.MapControllers ();

app.Run ();
