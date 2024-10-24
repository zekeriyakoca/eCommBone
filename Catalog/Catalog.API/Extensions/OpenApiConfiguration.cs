using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection;

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
                Title = "Catalog API",
                Description = "API with Versioning"
            });
        });
    }
}