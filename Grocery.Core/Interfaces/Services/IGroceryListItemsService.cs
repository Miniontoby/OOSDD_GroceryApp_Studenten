
using Grocery.Core.Models;

namespace Grocery.Core.Interfaces.Services
{
    public interface IGroceryListItemsService : IService<GroceryListItem>
    {
        public List<GroceryListItem> GetAllOnGroceryListId(int groceryListId);

        public List<BestSellingProducts> GetBestSellingProducts(int topX = 5);
    }
}
