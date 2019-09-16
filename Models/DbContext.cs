using Models.DatabaseSettings;
using Models.Entities;
using MongoDB.Driver;

namespace Models
{
    public class DbContext : IDbContext
    {
        public IMongoCollection<Subscription> Subscriptions { get; }
        public IMongoCollection<Message> Messages { get; }

        public DbContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Subscriptions = database.GetCollection<Subscription>(settings.SubscriptionsCollectionName);
            Messages = database.GetCollection<Message>(settings.MessagesCollectionName);
        }
    }
}