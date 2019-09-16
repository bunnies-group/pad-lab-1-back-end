using Models.DatabaseSettings;
using Models.Entities;
using MongoDB.Driver;

namespace Models
{
    public class DbContext : IDbContext
    {
        public IMongoCollection<Connection> Connections { get; }
        public IMongoCollection<Message> Messages { get; }

        public DbContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Connections = database.GetCollection<Connection>(settings.ConnectionsCollectionName);
            Messages = database.GetCollection<Message>(settings.MessagesCollectionName);
        }
    }
}