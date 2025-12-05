using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebpServer.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation("[Request] {Method} {Path}", request.Method, request.Path);

            await _next(context);   // 다음 미들웨어/컨트롤러 실행

            stopwatch.Stop();
            var statusCode = context.Response.StatusCode;

            _logger.LogInformation(
                "[Response] {Method} {Path} => {StatusCode} ({Elapsed} ms)",
                request.Method,
                request.Path,
                statusCode,
                stopwatch.ElapsedMilliseconds
            );
        }
    }
}
