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
            //context.Response.StatusCode = ex switch
            //{
            //    NotFoundException => StatusCodes.Status404NotFound,
            //    BadRequestException => StatusCodes.Status400BadRequest,
            //    _ => StatusCodes.Status500InternalServerError
            //};
            if (ex.GetType().Name == typeof(NotFoundException).Name)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else if (ex.GetType().Name == typeof(BadRequestException).Name)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            // here we can use logger
            var json = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message
            }.ToString(); // here i override ToString to serialize object directly

            await context.Response.WriteAsync(json);
        }

    }

}
