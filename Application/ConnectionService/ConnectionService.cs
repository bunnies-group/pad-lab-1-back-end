using System.Threading.Tasks;
using Models;
using Models.Entities;
using MongoDB.Driver;

namespace Application.ConnectionService
{
    public class ConnectionService : IConnectionService
    {
        private readonly IDbContext _dbContext;

        public ConnectionService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Connection> Get(string id) =>
            await _dbContext.Connections
                .Find(connection => connection.ConnectionId == id)
                .FirstOrDefaultAsync();

        public async Task Create(Connection connection) => 
            await _dbContext.Connections.InsertOneAsync(connection);

        public async Task Remove(string id) =>
            await _dbContext.Connections.DeleteOneAsync(connection => connection.ConnectionId == id);
    }
}