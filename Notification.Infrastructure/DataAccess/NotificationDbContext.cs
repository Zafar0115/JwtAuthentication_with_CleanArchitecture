using Microsoft.EntityFrameworkCore;
using Notification.Application.Abstraction;
using Notification.Domain.Models;

namespace Notification.Infrastructure.DataAccess
{
    public class NotificationDbContext : DbContext, IApplicationDbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
