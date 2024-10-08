
using Serilog;
using Serilog.Core;

namespace MyAPI
{
    //dotnet new webapi --no-https --auth=None
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = ConfigureSerilog("Logs/application.log");

            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.

            builder.Services.AddControllers();

            ConfigureVersioning(builder.Services);



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            //https://localhost:7113/swagger/index.html
            //http://localhost:7113/swagger/index.html
            //if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static Logger ConfigureSerilog(string fileName)
        {
            //To read from "appsettings.json"
            //Package; Serilog.Settings.Configuration  
            //var configuration = new ConfigurationBuilder() //REF2
            //    .AddJsonFile("appsettings.json")
            //    .Build();


            if (File.Exists(fileName))
                File.Delete(fileName);

            Logger log = new LoggerConfiguration()
                //.ReadFrom.Configuration(configuration) //REF2
                //else
                //.WriteTo.File(fileName)// , restrictedToMinimumLevel: LogEventLevel.Debug, rollingInterval: RollingInterval.Day)
                .WriteTo.File(fileName, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            return log;
        }

        private static void ConfigureVersioning(IServiceCollection services)
        {
            //https://www.c-sharpcorner.com/article/api-versioning-in-asp-net-core-web-api/

            //ad package Microsoft.AspNetCore.Mvc.Versioning

            //services.AddApiVersioning(x =>
            //{
            //    x.DefaultApiVersion = new ApiVersion(1, 0);
            //    x.AssumeDefaultVersionWhenUnspecified = true;
            //    x.ReportApiVersions = true;
            //});
        }
    }
}
