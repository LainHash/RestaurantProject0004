using Restaurant.Application.Common.Models;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Restaurant.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = HttpStatusCode.InternalServerError;
        var message = "An internal server error occurred.";

        switch (exception)
        {
            case Restaurant.Application.Common.Exceptions.ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                var errorMessages = validationException.Errors
                    .SelectMany(kvp => kvp.Value.Select(err => $"{kvp.Key}: {err}"));
                message = "Validation failed: " + string.Join(" | ", errorMessages);
                break;
            case KeyNotFoundException _:
                // case NotFoundException _: // Add custom NotFoundException if exists
                statusCode = HttpStatusCode.NotFound;
                message = "Resource not found.";
                break;
            default:
                message = exception.Message; // In production, might want to hide this
                break;
        }

        context.Response.StatusCode = (int)statusCode;
        var response = Result.Fail(message);

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        return context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }
}
