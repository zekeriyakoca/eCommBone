using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace Pricing.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // services.AddDbContext<CatalogDbContext>(options => { });
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAdB2C"));
        services.AddAuthorization();

        services.AddOpenApiServices();
        
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        }); 
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // Default to v1.0 in Swagger UI
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API v1.0");
            });
        }

        app.UseHttpsRedirection();

        var scopeRequiredByApi = Configuration["AzureAd:Scopes"] ?? "";

        app.UseRouting();
        // app.UseAuthentication();
        // app.UseAuthorization();
        
        app.UseEndpoints(endpoints =>
        {
            if (env.IsDevelopment())
            {
                endpoints.MapOpenApi();
            }

            endpoints.MapControllers();
        });
    }
}

public static partial class ConfigurationExtensions
{
    public static void AddOpenApiServices(this IServiceCollection services)
    {
        // Learn more about configuring OpenAPI
        services.AddOpenApi();

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0); // Set default version
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        // Add versioned API explorer (requires Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer)
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV"; // Format version as v1, v2, etc.
            options.SubstituteApiVersionInUrl = true;
        });

        // Add Swagger services
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Version = "v1",
                Title = "Pricing API",
                Description = "API with Versioning"
            });
        });
    }
}