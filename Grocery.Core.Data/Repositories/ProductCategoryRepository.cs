using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;

namespace Grocery.Core.Data.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly List<ProductCategory> productCategories;
        public ProductCategoryRepository()
        {
            productCategories = [
                new ProductCategory(1, 3, 1),
                new ProductCategory(2, 3, 2),
                new ProductCategory(3, 2, 3),
                new ProductCategory(4, 5, 4),
            ];
        }

        public List<ProductCategory> GetAll()
        {
            return productCategories;
        }

        public List<ProductCategory> GetAllOnCategoryId(int id)
        {
            return productCategories.FindAll(g => g.CategoryId == id);
        }

        public ProductCategory? Get(int id)
        {
            return productCategories.FirstOrDefault(p => p.Id == id);
        }

        public ProductCategory Add(ProductCategory item)
        {
            int newId = 1;
            try { newId = productCategories.Max(g => g.Id) + 1; }
            catch { }
            item.Id = newId;
            productCategories.Add(item);
            return Get(item.Id) ?? item;
        }

        public ProductCategory? Update(ProductCategory item)
        {
            ProductCategory? productCategory = productCategories.FirstOrDefault(p => p.Id == item.Id);
            if (productCategory is null) return null;
            productCategory = item;
            return productCategory;
        }

        public ProductCategory? Delete(ProductCategory item)
        {
            ProductCategory? productCategory = productCategories.FirstOrDefault(p => p.Id == item.Id);
            if (productCategory is null) return null;
            productCategories.Remove(productCategory);
            return productCategory;
        }
    }
}
