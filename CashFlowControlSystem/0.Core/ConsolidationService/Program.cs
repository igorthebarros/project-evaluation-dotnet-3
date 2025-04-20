using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Serilog;
using SharedKernel.HealthChecks;

namespace ConsolidationService;

public class Program
{
	public static void Main(string[] args)
	{
		try
		{
            Console.WriteLine("Starting Consolidation Service...");
            Log.Information("Starting Consolidation Service...");
			var builder = WebApplication.CreateBuilder(args);

			builder.WebHost.UseUrls("http://*:5002");

			builder.AddBasicHealthChecks();

			builder.Services.AddHealthChecks()
				.AddCheck("self", () =>
				HealthCheckResult.Healthy("Consolidation Service is running!"));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {

            }

            app.UseHttpsRedirection();

            app.UseBasicHealthChecks();

            app.Run();

        }
		catch (Exception e)
		{
            Console.WriteLine(e.Message);
            Log.Fatal(e, "Consolidation Service terminated unexpectedly.");
			throw;
		}
		finally
		{
			Log.CloseAndFlush();
		}
	}
}