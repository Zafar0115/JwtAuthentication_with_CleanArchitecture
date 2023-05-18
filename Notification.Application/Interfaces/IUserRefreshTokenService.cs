using Notification.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Application.Interfaces
{
    public interface IUserRefreshTokenService
    {
        Task<UserRefreshTokens?> AddUserRefreshTokens(UserRefreshTokens user);
        Task<UserRefreshTokens?> GetSavedRefreshTokens(UserCredentials credentials, string refreshtoken);
        Task<bool> DeleteUserRefreshTokens(UserCredentials credentials, string refreshToken);
    }
}
