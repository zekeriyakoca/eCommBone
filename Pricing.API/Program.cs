using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web.Resource;
using Pricing.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add Startup as a service
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline
var env = app.Environment;

startup.Configure(app, env);

if (env.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    // DataSeeder.Seed(scope.ServiceProvider.GetRequiredService<CatalogDbContext>()).Wait();
}

app.Run();