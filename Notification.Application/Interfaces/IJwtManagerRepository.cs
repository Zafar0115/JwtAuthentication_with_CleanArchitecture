using Notification.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Application.Interfaces
{
    public interface IJwtManagerRepository
    {
        Token? GenerateAccessTokens(UserCredentials userCredentials);
        Token? GenerateRefreshToken(UserCredentials userCredentials);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
