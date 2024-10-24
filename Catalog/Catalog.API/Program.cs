using Catalog.Infrastructure;

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
    DataSeeder.Seed(scope.ServiceProvider.GetRequiredService<CatalogDbContext>()).Wait();
}

app.Run();