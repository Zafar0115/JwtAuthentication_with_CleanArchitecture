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
        public DbSet<UserRefreshTokens> UserRefreshTokens { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = await base.SaveChangesAsync(cancellationToken);
            await Database.ExecuteSqlRawAsync("update user_refresh_tokens set is_active=false where expiration_date<(select current_timestamp)");
            return result;
        }
    }
}
