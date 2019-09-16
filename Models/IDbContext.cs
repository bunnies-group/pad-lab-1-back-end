using Models.Entities;
using MongoDB.Driver;

namespace Models
{
    public interface IDbContext
    {
        IMongoCollection<Connection> Connections { get; }
        IMongoCollection<Message> Messages { get; }
    }
}