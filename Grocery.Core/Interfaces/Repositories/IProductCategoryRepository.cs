using Grocery.Core.Models;

namespace Grocery.Core.Interfaces.Repositories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        public List<ProductCategory> GetAllOnCategoryId(int id);
    }
}
