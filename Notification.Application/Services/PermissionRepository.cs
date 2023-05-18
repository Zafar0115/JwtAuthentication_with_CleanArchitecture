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
    public class PermissionRepository : IPermissionRepository
    {
        private readonly IApplicationDbContext dbContext;

        public PermissionRepository(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Permission entity)
        {
            int count=dbContext.Permissions.ToList().Count;
            entity.Id= count+1;
            await dbContext.Permissions.AddAsync(entity);
            int affected=await dbContext.SaveChangesAsync();
            return affected > 0;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            Permission? permission=dbContext.Permissions.FirstOrDefault(o=>o.Id==id);
            if (permission is null) return false;
            dbContext.Permissions.Remove(permission);
            int affected= await dbContext.SaveChangesAsync();
            return affected > 0;
        }

        public Task<IQueryable<Permission>> GetAll(Expression<Func<Permission, bool>>? expression = null)
        {
            IQueryable<Permission> permissions=dbContext.Permissions;
            return Task.FromResult(permissions);
        }

        public Task<Permission?> GetById(int id)
        {
            Permission? permission=dbContext.Permissions.FirstOrDefault(o=>o.Id==id);
            return Task.FromResult(permission);
        }

        public async Task<bool> UpdateAsync(Permission entity)
        {
            Permission? permission = dbContext.Permissions.FirstOrDefault(o => o.Id == entity.Id);

            if(permission is null) return false;

            permission.PermissionName=entity.PermissionName;
            permission.Description=entity.Description;
            int affected=await dbContext.SaveChangesAsync();
            return affected > 0;
        }
    }
}
