using System.Text.Json;
using BaseCleanArchitecture.Domain.Exceptions;
using BaseCleanArchitecture.Domain.Exceptions.Error;
using BaseCleanArchitecture.Domain.Exceptions.ValidationError;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BaseCleanArchitecture.API.Middlewares.ExceptionHandlerMiddleware;

internal sealed class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;


    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
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
        catch (ErrorException ex)
        {
            _logger.LogError(ex, "An ErrorException occurred: {Message}", ex.Message);
            await HandleExceptionAsync(httpContext, ex);
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, "An ValidationException occurred: {Message}", ex.Message);
            await HandleExceptionAsync(httpContext, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An Exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(httpContext, ex);
        }
    }


    private static async Task HandleExceptionAsync(HttpContext httpContext, ErrorException exception)
    {
        var response = _response(exception);
        await _handel(httpContext, response, exception.StatusCode);
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, ValidationException exception)
    {
        var response = _response(exception);
        await _handel(httpContext, response, HttpStatusCode.Failed400);
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var response = _response(exception);
        await _handel(httpContext, response, HttpStatusCode.Exception500);
    }

    private static string _response(Exception exception)
        => JsonSerializer.Serialize(new
        {
            exception.Message
        });

    private static string _response(ValidationException exception)
        => JsonSerializer.Serialize(new List<IValidationError>()
        {
            exception
        });

    private static string _response(ErrorException exception)
        => JsonSerializer.Serialize(exception as IError);

    private static async Task _handel(HttpContext httpContext, string response, HttpStatusCode httpStatusCode)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)httpStatusCode;
        await httpContext.Response.WriteAsync(response);
    }
}