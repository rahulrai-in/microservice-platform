using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Microservices.TestAPI
{
    public class DummyStartupCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new()) => Task.FromResult(new HealthCheckResult(HealthStatus.Healthy));
    }
}