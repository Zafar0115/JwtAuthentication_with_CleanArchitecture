using Microsoft.EntityFrameworkCore;
using Notification.Application.Abstraction;
using Notification.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Infrastructure.DataAccess
{
    public class NotificationDbContext : DbContext,IApplicationDbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options): base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}
