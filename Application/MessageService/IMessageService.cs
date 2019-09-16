using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities;

namespace Application.MessageService
{
    public interface IMessageService
    {
        Task<IEnumerable<string>> GetByTopic(string topic);
        Task Create(Message message);
    }
}