using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Microservices.Platform
{
    public static class ServiceCollectionExtensions
    {
        private const string Liveness = "liveness";
        private const string Startup = "startup";

        public static IServiceCollection AddBasicHealthChecks(
            this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck("BasicStartupHealthCheck",
                    () => HealthCheckResult.Healthy(), new[] {Startup})
                .AddCheck("BasicLivenessHealthCheck",
                    () => HealthCheckResult.Healthy(), new[] {Liveness});

            return services;
        }

        public static IServiceCollection AddAdditionStartupHealthChecks<T>(
            this IServiceCollection services) where T : class, IHealthCheck
        {
            services.AddHealthChecks().AddCheck<T>(nameof(T), tags: new[]
            {
                Startup
            });
            return services;
        }

        public static IServiceCollection AddAdditionLivenessHealthChecks<T>(
            this IServiceCollection services) where T : class, IHealthCheck
        {
            services.AddHealthChecks().AddCheck<T>(nameof(T), tags: new[]
            {
                Liveness
            });
            return services;
        }
    }
}