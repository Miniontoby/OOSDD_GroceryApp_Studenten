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
                        Product? product = _productRepository.Get(groceryListItemItem.ProductId) ?? groceryListItemItem.Product;
                        if (product is not null)
                            boughtProductsList.Add(new(client, groceryListItem, product));
                    }
                }
            }

            return boughtProductsList;
        }
        
        /*
        public List<BoughtProducts> Get(int? productId)
        {
            List<BoughtProducts> boughtProductsList = [];
            var MyGroceryListItems = _groceryListItemsRepository.GetAll();
            if (productId.HasValue && MyGroceryListItems != null && MyGroceryListItems.Count > 0)
            {
                Product product = _productRepository.Get(productId.GetValueOrDefault());
                foreach (var item in MyGroceryListItems)
                {
                    if (item.ProductId == productId)
                    {
                        GroceryList groceryList = _groceryListRepository.Get(item.GroceryListId);
                        Client client = _clientRepository.Get(groceryList.ClientId);
                        boughtProductsList.Add(new(client, groceryList, product));
                    }
                }
            }

            return boughtProductsList;
        }
        */
    }
}
