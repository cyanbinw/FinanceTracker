using Autofac;
using Autofac.Extensions.DependencyInjection;
using FinanceTracker.EntityFramework;
using FinanceTracker.EntityFramework.Autofac;
using FinanceTracker.Service.Filter;
using FinanceTracker.Service.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using System.Reflection;

namespace FinanceTracker.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Early init of NLog to allow startup and exception logging, before host is built
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");

            try
            {
                var builder = WebApplication.CreateBuilder(args);


                builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

                // NLog: Setup NLog for Dependency injection
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                IConfiguration configuration = builder.Configuration;

                builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

                // Database
                builder.Services.AddDbContext<PostgreSqlContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("Postgresql"), c =>
                    {
                        c.MigrationsAssembly("FinanceTracker.EntityFramework");
                    }));

                builder.Services.AddDatabaseDeveloperPageExceptionFilter();

                builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
                    builder.RegisterModule<DataRegister>();
                });

                // Turn off default model validation
                builder.Services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);

                // Add services to the container.
                builder.Services.AddControllers(c =>
                {
                    // Add custom model validation
                    c.Filters.Add<ModelValidationAttribute>();
                });
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddOpenApiDocument();


                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {

                }

                // Add OpenAPI 3.0 document serving middleware
                // Available at: http://localhost:<port>/swagger/v1/swagger.json
                app.UseOpenApi();

                // Add web UIs to interact with the document
                // Available at: http://localhost:<port>/swagger
                app.UseSwaggerUi();

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }
    }
}
