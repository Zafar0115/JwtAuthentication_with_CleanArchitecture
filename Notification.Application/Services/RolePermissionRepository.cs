using Notification.Application.Abstraction;
using Notification.Application.Interfaces;
using Notification.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Application.Services
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly IApplicationDbContext dbContext;

        public RolePermissionRepository(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(RolePermission entity)
        {
            await dbContext.RolePermissions.AddAsync(entity);
            int affected = await dbContext.SaveChangesAsync();
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            RolePermission? rolePermission=dbContext.RolePermissions.FirstOrDefault(o=>o.Id== id);
            if (rolePermission is null) return false;

            dbContext.RolePermissions.Remove(rolePermission);
            int affected=await dbContext.SaveChangesAsync();
            return affected > 0;
        }

        public Task<IQueryable<RolePermission>> GetAll(Expression<Func<RolePermission, bool>>? expression = null)
        {
            IQueryable<RolePermission> rolePermissions=dbContext.RolePermissions;
            return Task.FromResult(rolePermissions);
        }

        public Task<RolePermission?> GetById(int id)
        {
            RolePermission? rolePermission=dbContext.RolePermissions.FirstOrDefault(o=>o.Id== id);
            return Task.FromResult(rolePermission);
        }

        public async Task<bool> UpdateAsync(RolePermission entity)
        {
            RolePermission? rolePermission = dbContext.RolePermissions.FirstOrDefault(o => o.Id == entity.Id);

            if (rolePermission is null) return false;
            rolePermission.Role=entity.Role;
            rolePermission.Permission=entity.Permission;
            int rowsAffected=await dbContext.SaveChangesAsync();
            return rowsAffected > 0;

        }
    }
}
