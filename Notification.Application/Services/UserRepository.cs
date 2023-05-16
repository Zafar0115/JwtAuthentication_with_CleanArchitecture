using Notification.Application.Abstraction;
using Notification.Application.Interfaces;
using Notification.Domain.Models;
using System.Linq.Expressions;

namespace Notification.Application.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext dbContext;

        public UserRepository(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(User entity)
        {
            await dbContext.Users.AddAsync(entity);
            int affectedRows = await dbContext.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            User? user = dbContext.Users.FirstOrDefault(o => o.Id == id);

            if (user != null)
            {
                dbContext.Users.Remove(user);
                int affectedRows = await dbContext.SaveChangesAsync();
                return affectedRows > 0;
            }

            return false;
        }

        public Task<IQueryable<User>> GetAll(Expression<Func<User, bool>>? expression = null)
        {
            IQueryable<User> users = dbContext.Users.AsQueryable();

            return Task.FromResult(users);
        }

        public Task<User?> GetById(int id)
        {
            User? user = dbContext.Users.FirstOrDefault(o => o.Id == id);
            return Task.FromResult(user);
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            User? user = dbContext.Users.FirstOrDefault(o => o.Id == entity.Id);

            if (user is null) return false;
            user.UserName = entity.UserName;
            user.Password=entity.Password;
            user.EmailAddress = entity.EmailAddress;
            user.FullName = entity.FullName;
            int affectedRows = await dbContext.SaveChangesAsync();

            return affectedRows > 0;

        }
    }
}
