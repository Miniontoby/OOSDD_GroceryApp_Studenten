using Grocery.Core.Models;

namespace Grocery.Core.Interfaces.Repositories
{
    public interface IGroceryListItemsRepository : IRepository<GroceryListItem>
    {
        public List<GroceryListItem> GetAllOnGroceryListId(int id);
    }
}
