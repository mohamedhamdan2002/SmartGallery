using Microsoft.AspNetCore.Http;
using SmartGallery.Server.Exceptions;
using SmartGallery.Shared;
using System.Drawing.Text;

namespace SmartGallery.Server.Middlewares;

public class GlobalErrorHandlerMiddleware : IMiddleware
{

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {

        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            var exceptionName = ex.GetType().Name;
            context.Response.StatusCode = exceptionName switch
            {
                nameof(NotFoundException) => StatusCodes.Status404NotFound,
                nameof(BadRequestException) => StatusCodes.Status400BadRequest,
                nameof(ConflictException) => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
            var json = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = context.Response.StatusCode !=
                            StatusCodes.Status500InternalServerError ?
                            ex.Message : "InternalServerError"
            }.ToString(); // here i override ToString to serialize object directly

            await context.Response.WriteAsync(json);
        }

    }

}
