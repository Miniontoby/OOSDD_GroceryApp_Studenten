using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;

namespace Grocery.Core.Data.Repositories
{
    public class GroceryListItemsRepository : IGroceryListItemsRepository
    {
        private readonly List<GroceryListItem> groceryListItems;

        public GroceryListItemsRepository()
        {
            groceryListItems = [
                new GroceryListItem(1, 1, 1, 3),
                new GroceryListItem(2, 1, 2, 1),
                new GroceryListItem(3, 1, 3, 4),
                new GroceryListItem(4, 2, 1, 2),
                new GroceryListItem(5, 2, 2, 5),
            ];
        }

        public List<GroceryListItem> GetAll()
        {
            return groceryListItems;
        }

        public List<GroceryListItem> GetAllOnGroceryListId(int id)
        {
            return groceryListItems.FindAll(g => g.GroceryListId == id);
        }

        public GroceryListItem? Get(int id)
        {
            return groceryListItems.FirstOrDefault(g => g.Id == id);
        }

        public GroceryListItem Add(GroceryListItem item)
        {
            int newId = 1;
            try { newId = groceryListItems.Max(g => g.Id) + 1; }
            catch { }
            item.Id = newId;
            groceryListItems.Add(item);
            return Get(item.Id) ?? item;
        }

        public GroceryListItem? Update(GroceryListItem item)
        {
            GroceryListItem? listItem = groceryListItems.FirstOrDefault(i => i.Id == item.Id);
            listItem = item;
            return listItem;
        }

        public GroceryListItem? Delete(GroceryListItem item)
        {
            GroceryListItem? groceryListItem = groceryListItems.FirstOrDefault(p => p.Id == item.Id);
            if (groceryListItem is null) return null;
            groceryListItems.Remove(groceryListItem);
            return groceryListItem;
        }
    }
}
