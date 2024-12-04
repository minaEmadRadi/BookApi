using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace BookRepo
{
    public class ErrorHandlingService
    {
        public static async Task HandleError(HttpContext context)
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature?.Error != null)
            {
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"An internal server error occurred: {exceptionHandlerPathFeature.Error.Message}, The error saved Successfully");
            }
        }
    }
}
