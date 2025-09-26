using Grocery.Core.Models;

namespace Grocery.Core.Interfaces.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        public Client? Get(string email);
    }
}
