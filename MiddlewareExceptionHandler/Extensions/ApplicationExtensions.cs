using Microsoft.AspNetCore.Builder;
using MiddlewareExceptionHandler.Common;

namespace MiddlewareExceptionHandler.Extensions
{
    public static class ApplicationExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(err => err.Run(MyExceptionHandler.Handle));
        }
    }
}