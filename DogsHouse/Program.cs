using AspNetCoreRateLimit;
using DogsHouse.Data;
using DogsHouse.Extensions;
using DogsHouse.Interfaces;
using DogsHouse.Services;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mySqlConnectionbuilder = new MySqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddDbContext<DataContext>(
    options => options.UseMySql(
        mySqlConnectionbuilder.ConnectionString,
        new MySqlServerVersion(new Version(8, 0)),
        x => x.MigrationsAssembly("DogsHouse.Data")
    )
);

builder.Services.AddScoped<DogRepository>();
builder.Services.AddScoped<IDogService, DogService>();

builder.Services.AddHealthChecks();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.SetupRateLimiter(builder.Configuration.GetSection("IpRateLimiting"));

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHealthChecks("_health");

app.UseAuthorization();

app.MapControllers();

app.UseIpRateLimiting();

app.Run();
