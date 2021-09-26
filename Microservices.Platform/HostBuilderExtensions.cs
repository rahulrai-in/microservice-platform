using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Formatting.Json;

namespace Microservices.Platform
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseLogging(this IHostBuilder builder)
        {
            return builder.UseSerilog((context, logger) =>
            {
                logger
                    .Enrich.FromLogContext()
                    .Enrich.WithSpan();

                if (context.HostingEnvironment.IsDevelopment())
                {
                    logger.WriteTo.Console(
                        outputTemplate:
                        "{Timestamp:yyyy-MM-dd HH:mm:ss} {TraceId} {Level:u3} {Message}{NewLine}{Exception}");
                }
                else
                {
                    logger.WriteTo.Console(new JsonFormatter());
                }
            });
        }

        public static IServiceCollection AddOtelServices(this IServiceCollection services, string applicationName)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            services.AddOpenTelemetryTracing(builder => builder
                .AddAspNetCoreInstrumentation()
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(applicationName))
                .AddHttpClientInstrumentation(options => options.SetHttpFlavor = true)
                .SetSampler(new AlwaysOnSampler())
                .AddConsoleExporter(options => options.Targets = ConsoleExporterOutputTargets.Console));

            return services;
        }
    }
}