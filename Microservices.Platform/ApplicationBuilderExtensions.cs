using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Microservices.Platform
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseKubernetesHealthChecks(this IApplicationBuilder app)
        {
            return
                app
                    .UseHealthChecks("/health/startup",
                        new HealthCheckOptions {Predicate = x => x.Tags.Contains("startup")})
                    .UseHealthChecks("/health/live",
                        new HealthCheckOptions {Predicate = x => x.Tags.Contains("liveness")});
        }
    }
}