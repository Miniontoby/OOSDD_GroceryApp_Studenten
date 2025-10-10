
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;

namespace Grocery.Core.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly List<Client> clientList;

        public ClientRepository()
        {
            clientList = [
                new Client(1, "M.J. Curie", "user1@mail.com", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08="),
                new Client(2, "H.H. Hermans", "user2@mail.com", "dOk+X+wt+MA9uIniRGKDFg==.QLvy72hdG8nWj1FyL75KoKeu4DUgu5B/HAHqTD2UFLU="),
                new Client(3, "A.J. Kwak", "user3@mail.com", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=", Role.Admin)
            ];
        }

        public List<Client> GetAll()
        {
            return clientList;
        }

        public Client? Get(string email)
        {
            return clientList.Find(client => client.EmailAddress.Equals(email));
        }

        public Client? Get(int id)
        {
            return clientList.Find(client => client.Id == id);
        }

        public Client Add(Client client)
        {
            int newId = 1;
            try { newId = clientList.Max(g => g.Id) + 1; }
            catch { }
            client.Id = newId;
            clientList.Add(client);
            return Get(client.Id) ?? client;
        }

        public Client? Update(Client item)
        {
            throw new NotImplementedException();
        }

        public Client? Delete(Client item)
        {
            throw new NotImplementedException();
        }
    }
}
