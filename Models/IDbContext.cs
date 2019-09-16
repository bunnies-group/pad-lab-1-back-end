using Models.Entities;
using MongoDB.Driver;

namespace Models
{
    public interface IDbContext
    {
        IMongoCollection<Subscription> Subscriptions { get; }
        IMongoCollection<Message> Messages { get; }
    }
}