using MediatR;
using Serilog;
using SharedKernel.HealthChecks;
using SharedKernel.Security;
using SharedKernel.Validation;

namespace ApiGateway;

public class Program
{
    public static void Main(string[] args)
    {
		try
		{
            Console.WriteLine("Starting ApiGateway Service...");
            Log.Information("Starting ApiGateway Service...");

            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseUrls("http://*:5000");

            // Validation pipeline
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // YARP
            builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

            // Configurable HealthCheck
            builder.AddBasicHealthChecks();

            // Security - JWT
            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseBasicHealthChecks();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
        catch (Exception e)
		{
            Console.WriteLine("ApiGateway terminated unexpectedly");
            Log.Fatal(e, "ApiGateway terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}