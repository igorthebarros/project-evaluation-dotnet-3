using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Serilog.Sinks.SystemConsole.Themes;
using System.Diagnostics;

namespace SharedKernel.Logging
{
    public static class LoggingExtension
    {
        static readonly DestructuringOptionsBuilder _destructuringOptionsBuilder = new DestructuringOptionsBuilder()
            .WithDefaultDestructurers()
            .WithDestructurers([new DbUpdateExceptionDestructurer()]);

        static readonly Func<LogEvent, bool> _filterPredicate = exclusionPredicate =>
        {

            if (exclusionPredicate.Level != LogEventLevel.Information) return true;

            exclusionPredicate.Properties.TryGetValue("StatusCode", out var statusCode);
            exclusionPredicate.Properties.TryGetValue("Path", out var path);

            var excludeByStatusCode = statusCode == null || statusCode.ToString().Equals("200");
            var excludeByPath = path?.ToString().Contains("/health") ?? false;

            return excludeByStatusCode && excludeByPath;
        };

        public static WebApplicationBuilder AddDefaultLogging(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration().CreateLogger();
            builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.WithMachineName()
                    .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
                    .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails(_destructuringOptionsBuilder)
                    .Filter.ByExcluding(_filterPredicate);

                if (Debugger.IsAttached)
                {
                    loggerConfiguration.Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached);
                    loggerConfiguration.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}",
                        theme: SystemConsoleTheme.Colored);
                }
                else
                {
                    loggerConfiguration
                        .WriteTo.Console
                        (
                            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}"
                        )
                        .WriteTo.File(
                            "logs/log-.txt",
                            rollingInterval: RollingInterval.Day,
                            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}"
                        );
                }
            });

            builder.Services.AddLogging();

            return builder;
        }

        public static WebApplication UseDefaultLogging(this WebApplication app)
        {
            var logger = app.Services.GetRequiredService<ILogger<Logger>>();

            var mode = Debugger.IsAttached ? "Debug" : "Release";
            logger.LogInformation("Logging enabled for '{Application}' on '{Environment}' - Mode: {Mode}",
                app.Environment.ApplicationName,
                app.Environment.EnvironmentName,
                mode);
            return app;

        }
    }
}
