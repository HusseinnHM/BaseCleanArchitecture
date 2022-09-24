using Microsoft.AspNetCore.Builder;

namespace BaseCleanArchitecture.API.Middlewares.ExceptionHandlerMiddleware;

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        => builder.UseMiddleware<ExceptionHandlerMiddleware>();
}