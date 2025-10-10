using Grocery.Core.Models;

namespace Grocery.Core.Interfaces.Services
{
    public interface IProductCategoryService : IService<ProductCategory>
    {
        public List<ProductCategory> GetAllOnCategoryId(int categoryId);
    }
}
