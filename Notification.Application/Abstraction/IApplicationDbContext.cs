using Microsoft.EntityFrameworkCore;
using Notification.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Application.Abstraction
{
    public interface IApplicationDbContext
    {
        DbSet<Message> Messages { get; set; }
        DbSet<Service> Services { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
