using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly IHostEnvironment _env;

        private readonly ILogger<ExceptionMiddleware> _logger;

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var stackTrace = (ex.StackTrace.Replace(":line", String.Empty).Split('\n')[0]).Trim().Split(' ');
                var fullPath = stackTrace[3];
                var fileName = fullPath.Substring(fullPath.LastIndexOf('\\')).Replace("\\", String.Empty);
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                var response = _env.IsDevelopment() ? new ResponseBuilderException(
                    statusCode,
                    int.Parse(stackTrace[stackTrace.Length - 1]),
                    ex.Message,
                    fileName,
                    fullPath,
                    stackTrace[1]
                )
                : new ResponseBuilderException(statusCode);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}