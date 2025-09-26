using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public List<Client> GetAll()
        {
            return _clientRepository.GetAll();
        }

        public Client? Get(string email)
        {
            return _clientRepository.Get(email);
        }

        public Client? Get(int id)
        {
            return _clientRepository.Get(id);
        }

        public Client Add(Client client)
        {
            return _clientRepository.Add(client);
        }

        public Client? Update(Client client)
        {
            return _clientRepository.Update(client);
        }

        public Client? Delete(Client client)
        {
            return _clientRepository.Delete(client);
        }
    }
}
