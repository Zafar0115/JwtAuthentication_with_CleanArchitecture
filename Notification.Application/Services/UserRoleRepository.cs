using Notification.Application.Abstraction;
using Notification.Application.Interfaces;
using Notification.Domain.Models;
using System.Linq.Expressions;

namespace Notification.Application.Services
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IApplicationDbContext dbContext;

        public UserRoleRepository(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(UserRole entity)
        {
            int count = dbContext.UserRoles.ToList().Count;
            entity.Id = count + 1;
            await dbContext.UserRoles.AddAsync(entity);
            int affected = await dbContext.SaveChangesAsync();
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            UserRole? userRole = dbContext.UserRoles.FirstOrDefault(o => o.Id == id);
            if (userRole is null) return false;
            dbContext.UserRoles.Remove(userRole);
            int affected = await dbContext.SaveChangesAsync();
            return affected > 0;
        }

        public Task<IQueryable<UserRole>> GetAll(Expression<Func<UserRole, bool>>? expression = null)
        {
            IQueryable<UserRole> userRoles = dbContext.UserRoles;
            return Task.FromResult(userRoles);
        }

        public Task<UserRole?> GetById(int id)
        {
            UserRole? userRole = dbContext.UserRoles.FirstOrDefault(o => o.Id == id);
            return Task.FromResult(userRole);
        }

        public async Task<bool> UpdateAsync(UserRole entity)
        {
            UserRole? userRole = dbContext.UserRoles.FirstOrDefault(o => o.Id == entity.Id);

            if (userRole is null) return false;

            userRole.User = entity.User;
            userRole.Role = entity.Role;

            int affected = await dbContext.SaveChangesAsync();
            return affected > 0;
        }
    }
}
