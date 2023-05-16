using Microsoft.EntityFrameworkCore;
using Notification.Application.Abstraction;
using Notification.Application.Interfaces;
using Notification.Domain.Models;

namespace Notification.Application.Services
{
    public class UserRefreshTokenService : IUserRefreshTokenService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IJwtManagerRepository _jwtManagerRepository;

        public UserRefreshTokenService(IApplicationDbContext dbContext, IJwtManagerRepository jwtManagerRepository)
        {
            this._dbContext = dbContext;
            this._jwtManagerRepository = jwtManagerRepository;
        }

        public async Task<UserRefreshTokens> AddUserRefreshTokens(UserRefreshTokens user)
        {
            _dbContext.UserRefreshTokens.Add(user);
           await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserRefreshTokens(UserCredentials credentials, string refreshToken)
        {
            var userRefreshTokens = _dbContext.UserRefreshTokens.Include(x => x.User)
                .FirstOrDefault(x => x.User.UserName == credentials.UserName &&
                                     x.User.EmailAddress == credentials.EmailAddress &&
                                     x.User.Password == credentials.Password&&
                                     x.RefreshToken==refreshToken);

            if (userRefreshTokens is null) return false;

            _dbContext.UserRefreshTokens.Remove(userRefreshTokens);
            await _dbContext.SaveChangesAsync();
            return true;

        }

        public async Task<UserRefreshTokens?> GetSavedRefreshTokens(UserCredentials credentials, string refreshToken)
        {
            var userRefreshTokens= _dbContext.UserRefreshTokens.Include(x => x.User).Select(x=>x)
                .FirstOrDefault(x => x.User.UserName == credentials.UserName &&
                                     x.User.EmailAddress == credentials.EmailAddress &&
                                     x.User.Password == credentials.Password &&
                                     x.RefreshToken == refreshToken);

           
            return userRefreshTokens;
        }
      
    }
}
