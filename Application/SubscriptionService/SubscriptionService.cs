using System.Threading.Tasks;
using Models;
using Models.Entities;
using MongoDB.Driver;

namespace Application.SubscriptionService
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IDbContext _dbContext;

        public SubscriptionService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Subscription> Get(string id) =>
            await _dbContext.Subscriptions
                .Find(connection => connection.SubscriptionId == id)
                .FirstOrDefaultAsync();

        public async Task Create(Subscription subscription) => 
            await _dbContext.Subscriptions.InsertOneAsync(subscription);

        public async Task Remove(string id) =>
            await _dbContext.Subscriptions.DeleteOneAsync(connection => connection.SubscriptionId == id);
    }
}