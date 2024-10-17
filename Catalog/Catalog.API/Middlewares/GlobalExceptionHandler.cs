using Catalog.Domain.Exceptions;

namespace Catalog.API.Middlewares;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An error occurred.");

        if (exception is IDomainException)
        {
            // Return 400 Bad Request for domain exceptions
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var result = new
            {
                error = "An unexpected error occurred."
            };

            return context.Response.WriteAsJsonAsync(result);
        }

        // Fallback to 500 Internal Server Error for other exceptions
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var genericError = new
        {
            error = "An unexpected error occurred."
        };

        return context.Response.WriteAsJsonAsync(genericError);
    }
}
