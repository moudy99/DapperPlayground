
using Dapper_VS_EFcore.Configurations;
using Dapper_VS_EFcore.Context;
using Dapper_VS_EFcore.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dapper_VS_EFcore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // add logging config => 
            builder.Logging.ClearProviders();
            builder.Host.SeriLogConfigure();
            ConfigurationManager configuration = builder.Configuration;

            // Add services to the container.

            builder.Services.AddControllers();


            builder.Services.DependencyServices(configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
