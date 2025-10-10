using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;

namespace Grocery.Core.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> products;
        public ProductRepository()
        {
            products = [
                new Product(1, "Melk", 300, 1.50m, new DateOnly(2025, 9, 25)),
                new Product(2, "Kaas", 100, 5.00m, new DateOnly(2025, 9, 30)),
                new Product(3, "Brood", 400, 1.99m, new DateOnly(2025, 9, 12)),
                new Product(4, "Cornflakes", 0, 3.49m, new DateOnly(2025, 12, 31)),
            ];
        }

        public List<Product> GetAll()
        {
            return products;
        }

        public Product? Get(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        public Product Add(Product item)
        {
            int newId = products.Max(g => g.Id) + 1;
            item.Id = newId;
            products.Add(item);
            return Get(item.Id) ?? item;
        }

        public Product? Update(Product item)
        {
            Product? product = products.FirstOrDefault(p => p.Id == item.Id);
            if (product is null) return null;
            product = item;
            return product;
        }

        public Product? Delete(Product item)
        {
            throw new NotImplementedException();
        }
    }
}
