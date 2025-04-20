using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System.Net.Mime;

namespace SharedKernel.HealthChecks
{
    public static class HealthCheckExtension
    {
        public static void AddBasicHealthChecks(this WebApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks()
                .AddCheck("Liveness", () => HealthCheckResult.Healthy(), tags: ["liveness"])
                .AddCheck("Readiness", () => HealthCheckResult.Healthy(), tags: ["readiness"]);
        }

        public static void UseBasicHealthChecks(this WebApplication app)
        {
            var livenessOptions = WriteHealtCheckRespose(app, "liveness");
            app.UseHealthChecks("/health/live", livenessOptions);

            var readinessOptions = WriteHealtCheckRespose(app, "readiness");
            app.UseHealthChecks("/health/ready", readinessOptions);

            var healthOptions = WriteHealtCheckRespose(app, string.Empty);
            app.UseHealthChecks("/health", healthOptions);

            var logger = app.Services.GetRequiredService<ILogger<HealthCheckService>>();
            logger.LogInformation("Health Check enabled at: '/health'");
        }

        private static HealthCheckOptions WriteHealtCheckRespose(this WebApplication app, string tag)
        {
            var options = new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains(tag),
                ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            },
                ResponseWriter = async (context, report) =>
                {
                    var result = new
                    {
                        status = report.Status.ToString(),
                        healthChecks = report.Entries.Select(e => new
                        {
                            name = e.Key,
                            status = e.Value.Status.ToString(),
                            description = e.Value.Description,
                            errorMessage = e.Value.Exception?.Message,
                            hostEnvironment = app.Environment.EnvironmentName.ToLowerInvariant()
                        }),
                    };
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsJsonAsync(result);
                },
            };

            return options;
        }
    }
}
