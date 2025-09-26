using Grocery.Core.Models;

namespace Grocery.Core.Interfaces.Services
{
    public interface IClientService : IService<Client>
    {
        public Client? Get(string email);
    }
}
