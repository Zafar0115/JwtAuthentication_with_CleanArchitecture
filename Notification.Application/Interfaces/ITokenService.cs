using Microsoft.Extensions.Configuration;
using Notification.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string?> CreateTokenAsync(UserCredentials credentials);

    }
}
