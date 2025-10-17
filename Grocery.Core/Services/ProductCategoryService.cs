using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public List<ProductCategory> GetAll()
        {
            return _productCategoryRepository.GetAll();
        }

        public List<ProductCategory> GetAllOnCategoryId(int categoryId)
        {
            List<ProductCategory> productCategories = _productCategoryRepository.GetAllOnCategoryId(categoryId);
            FillService(productCategories);
            return productCategories;
        }

        public ProductCategory? Get(int id)
        {
            return _productCategoryRepository.Get(id);
        }

        public ProductCategory Add(ProductCategory item)
        {
            return _productCategoryRepository.Add(item);
        }

        public ProductCategory? Update(ProductCategory item)
        {
            return _productCategoryRepository.Update(item);
        }

        public ProductCategory? Delete(ProductCategory item)
        {
            return _productCategoryRepository.Delete(item);
        }

        private void FillService(List<ProductCategory> productCategories)
        {
            foreach (ProductCategory pc in productCategories)
            {
                pc.Product = _productRepository.Get(pc.ProductId) ?? new(0, "", 0);
                pc.Category = _categoryRepository.Get(pc.CategoryId) ?? new(0, "");
            }
        }
    }
}
