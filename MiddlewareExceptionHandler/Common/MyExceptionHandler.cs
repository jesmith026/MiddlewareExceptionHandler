using System;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using MiddlewareExceptionHandler.Exceptions;
using Newtonsoft.Json;

namespace MiddlewareExceptionHandler.Common
{
    public static class MyExceptionHandler
    {
        public static async Task Handle(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

            if (contextFeature != null)
            {
                var statusCode = HttpStatusCode.InternalServerError;

                switch (contextFeature.Error)
                {
                    case NotImplementedException ex:
                        statusCode = HttpStatusCode.NotImplemented;
                        break;
                    case ArgumentNullException ex:
                        statusCode = HttpStatusCode.BadRequest;
                        break;
                    case AuthenticationException ex:
                        statusCode = HttpStatusCode.Unauthorized;
                        break;
                    case NotFoundException ex:
                        statusCode = HttpStatusCode.NotFound;
                        break;
                }

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    StatusCode = statusCode,
                    Message = contextFeature.Error.Message
                }));
            }
        }
    }
}