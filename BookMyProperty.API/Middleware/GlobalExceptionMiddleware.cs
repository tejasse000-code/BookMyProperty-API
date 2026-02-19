using System.Net;
using System.Text.Json;
using BookMyProperty.Application.Exceptions;
using BookMyProperty.API.Models;

namespace BookMyProperty.API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
            _logger.LogError($"An unhandled exception has occurred: {ex.Message}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ApiResponse<object>();

        switch (exception)
        {
            case NotFoundException notFoundEx:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Success = false;
                response.Message = notFoundEx.Message;
                break;

            case ValidationException validationEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = validationEx.Message;
                break;

            case UnauthorizedAccessException unauthorizedEx:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response.Success = false;
                response.Message = unauthorizedEx.Message;
                break;

            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Success = false;
                response.Message = "An internal server error occurred.";
                break;
        }

        return context.Response.WriteAsJsonAsync(response);
    }
}

