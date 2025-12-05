using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WebpServer.Exceptions;

namespace WebpServer.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ErrorHandling] Unhandled exception");

                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("[ErrorHandling] Response has already started.");
                    throw;
                }

                context.Response.Clear();
                context.Response.ContentType = "application/json";

                var statusCode = HttpStatusCode.InternalServerError;
                string errorCode = "InternalError";
                string message = "Internal Server Error";

                // 도메인 예외 타입에 따라 분기
                switch (ex)
                {
                    case GameValidationException v:
                        statusCode = HttpStatusCode.BadRequest; // 400
                        errorCode = "ValidationError";
                        message = v.Message;
                        break;

                    case GameUnauthorizedException u:
                        statusCode = HttpStatusCode.Unauthorized; // 401
                        errorCode = "Unauthorized";
                        message = u.Message;
                        break;

                    default:
                        // 기타 예외는 그대로 500
                        break;
                }

                context.Response.StatusCode = (int)statusCode;

                var payload = new
                {
                    status = context.Response.StatusCode,
                    code = errorCode,
                    error = message,
                    traceId = context.TraceIdentifier
                };

                var json = JsonSerializer.Serialize(payload);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
