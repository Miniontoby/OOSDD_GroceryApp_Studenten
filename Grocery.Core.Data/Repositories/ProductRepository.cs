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
                new(1, "Melk", 300),
                new(2, "Kaas", 100),
                new(3, "Brood", 400),
                new(4, "Cornflakes", 0),
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
            product = item;
            return product;
        }

        public Product? Delete(Product item)
        {
            throw new NotImplementedException();
        }
    }
}
