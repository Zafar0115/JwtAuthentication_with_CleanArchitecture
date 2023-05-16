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
    public class ServiceRepository : IServiceRepository
    {
        private readonly IApplicationDbContext dbContext;

        public ServiceRepository(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Service entity)
        {
            dbContext.Services.Add(entity);
            int change = await dbContext.SaveChangesAsync();

            if (change > 0)
                return true;
            return false;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            Service? service = dbContext.Services.FirstOrDefault(m => m.Id == id);
            dbContext.Services.Remove(service);
            int changes = await dbContext.SaveChangesAsync();

            if (changes > 0) return true;
            return false;
        }

        public Task<IQueryable<Service>> GetAll(Expression<Func<Service, bool>>? expression = null)
        {
            IQueryable<Service> services = dbContext.Services;
            return Task.FromResult(services);
        }

        public Task<Service?> GetById(int id)
        {
            Service? service = dbContext.Services.FirstOrDefault(m => m.Id == id);

            return Task.FromResult(service);
        }

        public async Task<bool> UpdateAsync(Service entity)
        {
            Service? service = dbContext.Services.FirstOrDefault(m => m.Id == entity.Id);

            if (service is not null)
            {
                service.ServiceIp = entity.ServiceIp;
                service.ServiceName = entity.ServiceName;
            }

            int affected = await dbContext.SaveChangesAsync();

            if (affected > 0)
                return true;

            return false;
        }
    }
}
