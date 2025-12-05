using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebpServer.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private const string HeaderName = "X-Correlation-Id";
        private readonly RequestDelegate _next;
        private readonly ILogger<CorrelationIdMiddleware> _logger;

        public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 클라이언트가 이미 헤더를 준 경우: 그대로 사용
            // 없으면 새로 생성
            if (!context.Request.Headers.TryGetValue(HeaderName, out var correlationId) ||
                string.IsNullOrWhiteSpace(correlationId))
            {
                correlationId = Guid.NewGuid().ToString("N");
                context.Request.Headers[HeaderName] = correlationId;
            }

            // 응답 헤더에도 넣어주기
            context.Response.Headers[HeaderName] = correlationId;

            // HttpContext.Items에 저장해두고, 다른 미들웨어/컨트롤러에서 꺼내 쓸 수도 있음
            context.Items[HeaderName] = correlationId.ToString();

            _logger.LogInformation("[Correlation] {CorrelationId}", correlationId.ToString());

            await _next(context);
        }
    }
}
