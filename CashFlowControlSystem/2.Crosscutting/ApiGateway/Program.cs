using IoC;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ORM;
using Serilog;
using SharedKernel.Commands.AuthenticateUser;
using SharedKernel.Common;
using SharedKernel.Enums;
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

            // Custom IoC
            builder.RegisterDependencies();

            // Automapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            // Mediatr
            builder.Services.AddMediatR(x =>
            {
                x.RegisterServicesFromAssemblies(
                    typeof(Program).Assembly,
                    typeof(AuthenticateUserHandler).Assembly
                );
            });

            // Infrastructure - ORM Database
            builder.Services.AddDbContext<DefaultContext>(x =>
                x.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("ORM")
                )
            );

            // Infrastructure - Cache Database
            // ....

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<DefaultContext>();
                    context.Database.Migrate();

                    // Populate User in order to test Authentication endpoint
                    // Avoid having to manually insert
                    if (!context.Users.Any())
                    {
                        context.Users.Add(
                            new User { 
                                Id = Guid.Parse("4e70cc6d-b139-4e26-9d8d-ecce10614531"),
                                Username = "Ian Anderson",
                                Email = "developer@developer.com",
                                Password = BCrypt.Net.BCrypt.HashPassword("$3cUr3"),
                                Phone = "+55988888888",
                                Role = UserRole.Admin,
                                Status = UserStatus.Active,
                                CreatedAt = DateTime.UtcNow,
                            }
                        );
                        context.SaveChanges();
                    }
                }

                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "my api v1");
                });
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