using System.Net;
using System.Text.Json;

namespace ConwayGameOfLife.API.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                await HandleExceptionAsync(context, exception);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var result = new
            {
                succeeded = false,
                data = default(object),
                errors = new List<string> {
                    exception.InnerException is not null ? exception.InnerException.Message : exception.Message,
                }
            };

            var resultSerialized = JsonSerializer.Serialize(result);

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(resultSerialized);
            }
        }
    }
}
