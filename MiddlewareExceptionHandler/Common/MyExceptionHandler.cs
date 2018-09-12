using System;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using MiddlewareExceptionHandler.Exceptions;
using Newtonsoft.Json;

namespace MiddlewareExceptionHandler.Common
{
    public static class MyExceptionHandler
    {
        public static async Task HandleAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

            if (contextFeature != null)
            {
                var statusCode = HttpStatusCode.InternalServerError;

                switch (contextFeature.Error)
                {
                    case NotImplementedException _:
                        statusCode = HttpStatusCode.NotImplemented;
                        break;
                    case ArgumentNullException _:
                        statusCode = HttpStatusCode.BadRequest;
                        break;
                    case AuthenticationException _:
                        statusCode = HttpStatusCode.Unauthorized;
                        break;
                    case NotFoundException ex:
                        statusCode = HttpStatusCode.NotFound;
                        break;
                }

                context.Response.StatusCode = (int)statusCode;

                var message = FlattenErrorMessages(contextFeature.Error);

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    StatusCode = statusCode,
                    Message = message,
                    StackTrace = contextFeature.Error.StackTrace
                }));
            }
        }

        private static string FlattenErrorMessages(Exception exception)
        {
            var msg = new StringBuilder();

            msg.Append(exception.Message);
            var innerException = exception.InnerException;

            while (innerException != null && !string.IsNullOrEmpty(innerException.Message))
            {
                msg.Append(" | ");
                msg.Append(innerException.Message);
                innerException = innerException.InnerException;
            }

            return msg.ToString();
        }
    }
}