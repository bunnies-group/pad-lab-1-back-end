using System.Threading.Tasks;
using Models.Entities;

namespace Application.ConnectionService
{
    public interface IConnectionService
    {
        Task<Connection> Get(string id);
        Task Create(Connection connection);
        Task Remove(string id);
    }
}