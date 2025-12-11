using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using System;
using WebpServer.External;
using WebpServer.Repositories;
using WebpServer.Service;
using WebpServer.Services;

namespace WebpServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWebpServer(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, InMemoryUserRepository>();
            services.AddScoped<IUserService, UserService>();


            services.AddSingleton<IScoreRepository, InMemoryScoreRepository>();
            services.AddScoped<IScoreService, ScoreService>();

            services.AddScoped<ITimeService, TimeService>();
            services
                .AddHttpClient<TimeApiClient>()
                .AddResilienceHandler("timeapi", builder =>
                {
                    // 1) 전체 요청 타임아웃
                    builder.AddTimeout(TimeSpan.FromSeconds(3));

                    // 2) 재시도 전략
                    builder.AddRetry(new HttpRetryStrategyOptions
                    {
                        MaxRetryAttempts = 3,
                        Delay = TimeSpan.FromMilliseconds(200),
                        BackoffType = DelayBackoffType.Exponential,
                        UseJitter = true
                    });
                });

            return services;
        }
    }
}
