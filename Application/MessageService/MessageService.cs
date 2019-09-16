using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.Entities;
using MongoDB.Driver;

namespace Application.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly IDbContext _dbContext;

        public MessageService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> GetByTopic(string topic) =>
            await _dbContext.Messages
                .Find(message => message.Topic == topic)
                .Project(message => message.Content)
                .ToListAsync();

        public async Task Create(Message message) =>
            await _dbContext.Messages.InsertOneAsync(message);
    }
}