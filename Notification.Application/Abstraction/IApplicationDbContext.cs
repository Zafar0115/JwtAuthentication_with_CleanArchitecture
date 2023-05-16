using Microsoft.EntityFrameworkCore;
using Notification.Domain.Models;

namespace Notification.Application.Abstraction
{
    public interface IApplicationDbContext
    {
        DbSet<Message> Messages { get; set; }
        DbSet<Service> Services { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<RolePermission> RolePermissions { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<UserRefreshTokens> UserRefreshTokens { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
