using Backend.Shared.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Backend.Shared.Presentation.Filters;

public class GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var (statusCode, message) = context.Exception switch
        {
            NotFoundException ex => (StatusCodes.Status404NotFound, ex.Message),
            ConflictException ex => (StatusCodes.Status409Conflict, ex.Message),
            DomainException ex => (StatusCodes.Status422UnprocessableEntity, ex.Message),
            _ => (StatusCodes.Status500InternalServerError, "Internal server error"),
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            logger.LogError(context.Exception, "Unhandled exception");
        }

        context.Result = new ObjectResult(new { error = message })
        {
            StatusCode = statusCode,
        };
        context.ExceptionHandled = true;
    }
}
