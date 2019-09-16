using System.Threading.Tasks;
using Models.Entities;

namespace Application.SubscriptionService
{
    public interface ISubscriptionService
    {
        Task<Subscription> Get(string id);
        Task Create(Subscription subscription);
        Task Remove(string id);
    }
}