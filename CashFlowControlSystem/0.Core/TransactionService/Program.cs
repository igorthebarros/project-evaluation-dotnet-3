using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Serilog;
using SharedKernel.HealthChecks;

namespace TransactionService;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Starting Transaction Service...");
            Log.Information("Starting Transaction Service...");
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseUrls("http://*:5001");

            builder.AddBasicHealthChecks();

            builder.Services.AddHealthChecks()
                .AddCheck("self", () =>
                HealthCheckResult.Healthy("Transaction Service is running!"));

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
            Log.Fatal(e, "Transaction Service terminated unexpectedly.");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}