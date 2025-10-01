
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class BoughtProductsService : IBoughtProductsService
    {
        private readonly IGroceryListItemsRepository _groceryListItemsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGroceryListRepository _groceryListRepository;
        public BoughtProductsService(IGroceryListItemsRepository groceryListItemsRepository, IGroceryListRepository groceryListRepository, IClientRepository clientRepository, IProductRepository productRepository)
        {
            _groceryListItemsRepository=groceryListItemsRepository;
            _groceryListRepository=groceryListRepository;
            _clientRepository=clientRepository;
            _productRepository=productRepository;
        }

        public List<BoughtProducts> Get(int? productId)
        {
            List<BoughtProducts> boughtProductsList = [];
            List<GroceryList> groceryList = _groceryListRepository.GetAll();

            foreach (Client client in _clientRepository.GetAll())
            {
                List<GroceryList> groceryListOnClientId = groceryList.FindAll(g => g.ClientId == client.Id);
                foreach (GroceryList groceryListItem in groceryListOnClientId)
                {
                    List<GroceryListItem> groceryListItemsList = _groceryListItemsRepository
                        .GetAllOnGroceryListId(groceryListItem.Id)
                        .FindAll(g => g.ProductId == productId);

                    foreach (GroceryListItem groceryListItemItem in groceryListItemsList)
                    {
                        Product? product = _productRepository.Get(groceryListItemItem.Id);
                        if (product is not null)
                            boughtProductsList.Add(new BoughtProducts(client, groceryListItem, product));
                    }
                }
            }

            return boughtProductsList;
        }
    }
}
