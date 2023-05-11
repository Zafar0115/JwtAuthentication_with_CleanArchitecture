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

    public class MessageRepository : IMessageRepository
    {
        private readonly IApplicationDbContext dbContext;

        public MessageRepository(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Message entity)
        {
             dbContext.Messages.Add(entity);
            int change=await dbContext.SaveChangesAsync();

            if (change > 0)
                return true;
            return false;
            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Message? message=dbContext.Messages.FirstOrDefault(m=>m.Id==id);
            dbContext.Messages.Remove(message);
            int changes=await dbContext.SaveChangesAsync();

            if(changes > 0) return true;
            return false;
        }

        public Task<IQueryable<Message>> GetAllAsync(Expression<Func<Message, bool>>? expression = null)
        {
            IQueryable<Message> messages=dbContext.Messages;
            return Task.FromResult(messages);
        }

        public Task<Message> GetByIdAsync(int id)
        {
            Message? message=dbContext.Messages.FirstOrDefault(m=>m.Id== id);

            return Task.FromResult(message);
        }

        public async Task<bool> UpdateAsync(Message entity)
        {
            Message? message = dbContext.Messages.FirstOrDefault(m => m.Id == entity.Id);

            if(message is not null)
            {
                message.UserId = entity.UserId;
                message.Service = entity.Service;
                message.NotificationStatus=entity.NotificationStatus;   
                message.Date=entity.Date;
            }

            int affected=await dbContext.SaveChangesAsync();

            if (affected > 0)
                return true;

            return false;
        }
    }
}
