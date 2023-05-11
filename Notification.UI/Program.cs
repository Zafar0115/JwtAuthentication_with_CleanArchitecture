using Microsoft.OpenApi.Models;
using Notification.Application;
using Notification.Domain.Models;
using Notification.Infrastructure;

namespace Notification.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            IConfiguration configuration = builder.Configuration;
            builder.Services.AddInfrastructure(configuration);
            builder.Services.AddApplication(configuration);

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });


            var app = builder.Build();


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
            });


            app.MapControllers();
            app.UseFileServer();
            app.UseRouting();
            app.UseStaticFiles();

            app.Run();
        }
    }
}