using Newtonsoft.Json;
using System.Net;

namespace SuperFarmer.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Super Farmer Request: Nieobsłużony wyjątek - Request {Name}", context.Request.Path);

                await HandleExcepionAsync(context, exception).ConfigureAwait(false);
            }
        }

        private Task HandleExcepionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message
            });
            context.Response.Redirect($"{context.Request.Scheme}://{context.Request.Host}/Error");

            return context.Response.WriteAsync(result);

        }
    }
}
