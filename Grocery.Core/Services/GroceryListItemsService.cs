using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class GroceryListItemsService : IGroceryListItemsService
    {
        private readonly IGroceryListItemsRepository _groceriesRepository;
        private readonly IProductRepository _productRepository;

        public GroceryListItemsService(IGroceryListItemsRepository groceriesRepository, IProductRepository productRepository)
        {
            _groceriesRepository = groceriesRepository;
            _productRepository = productRepository;
        }

        public List<GroceryListItem> GetAll()
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public List<GroceryListItem> GetAllOnGroceryListId(int groceryListId)
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll().Where(g => g.GroceryListId == groceryListId).ToList();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public GroceryListItem? Get(int id)
        {
            return _groceriesRepository.Get(id);
        }

        public GroceryListItem Add(GroceryListItem item)
        {
            return _groceriesRepository.Add(item);
        }

        public GroceryListItem? Update(GroceryListItem item)
        {
            return _groceriesRepository.Update(item);
        }

        public GroceryListItem? Delete(GroceryListItem item)
        {
            return _groceriesRepository.Delete(item);
        }

        public List<BestSellingProducts> GetBestSellingProducts(int topX = 5)
        {
            Dictionary<int, BestSellingProducts> bestSellingProducts = [];
            List<GroceryListItem> groceryListItems = GetAll();

            foreach (GroceryListItem groceryListItem in groceryListItems)
            {
                BestSellingProducts? bestSellingProduct;
                if (bestSellingProducts.TryGetValue(groceryListItem.Product.Id, out bestSellingProduct))
                {
                    bestSellingProduct.NrOfSells += groceryListItem.Amount;
                }
                else
                {
                    bestSellingProduct = new BestSellingProducts(
                        productId: groceryListItem.Product.Id,
                        name: groceryListItem.Product.Name,
                        stock: groceryListItem.Product.Stock,
                        nrOfSells: groceryListItem.Amount,
                        ranking: -1
                    );
                    bestSellingProducts.Add(groceryListItem.Product.Id, bestSellingProduct);
                }
            }

            List<BestSellingProducts> bestSellingProductsList = bestSellingProducts.Values.ToList();
            bestSellingProductsList.Sort((a, b) => b.NrOfSells.CompareTo(a.NrOfSells));

            for (int i = 0; i < bestSellingProductsList.Count; i++)
            {
                bestSellingProductsList[i].Ranking = i;
            }

            return bestSellingProductsList.Take(topX).ToList();
        }

        private void FillService(List<GroceryListItem> groceryListItems)
        {
            foreach (GroceryListItem g in groceryListItems)
            {
                g.Product = _productRepository.Get(g.ProductId) ?? new(0, "", 0);
            }
        }
    }
}
