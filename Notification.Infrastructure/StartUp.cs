using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Abstraction;
using Notification.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Infrastructure
{
    public static class StartUp
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<NotificationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DbConnection")));
            services.AddScoped<IApplicationDbContext,NotificationDbContext>();
            return services;
        }
    }
}
