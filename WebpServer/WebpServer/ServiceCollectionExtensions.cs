using Microsoft.Extensions.DependencyInjection;
using WebpServer.Repositories;
using WebpServer.Services;

namespace WebpServer
{
    // DI 등록을 모아두는 확장 메서드
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWebpServer(this IServiceCollection services)
        {
            // Service 계층 등록
            services.AddScoped<IUserService, UserService>();

            // Repository 계층 등록
            services.AddScoped<IUserRepository, InMemoryUserRepository>();

            return services;
        }
    }
}
