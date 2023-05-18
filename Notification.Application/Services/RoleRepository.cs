using Notification.Application.Abstraction;
using Notification.Application.Interfaces;
using Notification.Domain.Models;
using System.Linq.Expressions;

namespace Notification.Application.Services
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IApplicationDbContext dbContext;

        public RoleRepository(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Role entity)
        {
            int count = dbContext.Roles.ToList().Count;
            entity.Id = count + 1;
            await dbContext.Roles.AddAsync(entity);
            int affectedRows = await dbContext.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Role? role = dbContext.Roles.FirstOrDefault(o => o.Id == id);
            if (role is null) return false;

            dbContext.Roles.Remove(role);   
            int affectedRows=await dbContext.SaveChangesAsync();
            return affectedRows > 0;

        }

        public Task<IQueryable<Role>> GetAll(Expression<Func<Role, bool>>? expression = null)
        {
            IQueryable<Role> roles=dbContext.Roles.AsQueryable();
            return Task.FromResult(roles);
        }

        public Task<Role?> GetById(int id)
        {
           Role? role=dbContext.Roles.FirstOrDefault(r=> r.Id == id);
            return Task.FromResult(role);
        }

        public async Task<bool> UpdateAsync(Role entity)
        {
            Role? role = dbContext.Roles.FirstOrDefault(r => r.Id == entity.Id);
            if (role is null) return false;
            role.RoleName = entity.RoleName;
            int affected=await dbContext.SaveChangesAsync();    
            return affected > 0;
        }
    }
}
