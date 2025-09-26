using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IClientService _clientService;
        public AuthService(IClientService clientService)
        {
            _clientService = clientService;
        }

        public Client? Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            //Vraag de klantgegevens [Client] op die je zoekt met het opgegeven emailadres
            Client? client = _clientService.Get(email);

            //Als je een klant gevonden hebt controleer dan of het password matcht --> PasswordHelper.VerifyPassword(password, passwordFromClient)
            bool passwordMatched = client is not null && PasswordHelper.VerifyPassword(password, client.Password);

            //Als alles klopt dan klantgegveens teruggeven, anders null
            if (passwordMatched)
                return client;

            return null;
        }

        public Client? Register(string name, string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            //Vraag de klantgegevens [Client] op die je zoekt met het opgegeven emailadres
            Client? client = _clientService.Get(email);

            if (client is not null)
            {
                // Klant bestaat al, niet toestaan!
                return null;
            }

            string hashedPassword = PasswordHelper.HashPassword(password);

            client = new Client(0, name, email, hashedPassword);
            return _clientService.Add(client);
        }
    }
}
