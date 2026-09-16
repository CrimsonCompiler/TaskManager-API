using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using TaskManager.Api.Exceptions;

namespace TaskManager.Api.Services;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "An unexpected error occurred. Please try again later.";

        if (exception is NotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
            message = exception.Message;
        }
        else if (exception is BadHttpRequestException)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = exception.Message;
        }

        httpContext.Response.StatusCode = (int)statusCode;

        var errorResponse = new
        {
            StatusCode = (int)statusCode,
            Message = message,
            Timestamp = DateTime.UtcNow
        };

        await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

        return true;
    }
}