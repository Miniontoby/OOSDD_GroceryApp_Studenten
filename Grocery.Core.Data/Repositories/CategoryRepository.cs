using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;

namespace Grocery.Core.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly List<Category> categories;
        public CategoryRepository()
        {
            categories = [
                new(1, "Groente"),
                new(2, "Bakkerij"),
                new(3, "Zuivel"),
                new(4, "Conserven"),
                new(5, "Ontbijt")
            ];
        }

        public List<Category> GetAll()
        {
            return categories;
        }

        public Category? Get(int id)
        {
            return categories.FirstOrDefault(p => p.Id == id);
        }

        public Category Add(Category item)
        {
            int newId = 1;
            try { newId = categories.Max(g => g.Id) + 1; }
            catch { }
            item.Id = newId;
            categories.Add(item);
            return Get(item.Id) ?? item;
        }

        public Category? Update(Category item)
        {
            Category? category = categories.FirstOrDefault(p => p.Id == item.Id);
            if (category is null) return null;
            category = item;
            return category;
        }

        public Category? Delete(Category item)
        {
            throw new NotImplementedException();
        }
    }
}
