using Catalog.API.Behaviours;
using Catalog.API.Middlewares;
using Catalog.API.Validations;
using Catalog.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

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
        services.AddDbContext<CatalogDbContext>(options => { });
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAdB2C"));
        services.AddAuthorization();

        services.AddOpenApiServices();
        
        // MediatR
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        }); 
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); 
        
        services.AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>(); // FluentValidation

        services.AddApplicationServices();
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

        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

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