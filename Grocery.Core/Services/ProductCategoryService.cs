using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductRepository _productRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IProductRepository productRepository)
        {
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
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
            foreach (ProductCategory g in productCategories)
            {
                g.Product = _productRepository.Get(g.ProductId) ?? new(0, "", 0, 0.00m);
            }
        }
    }
}
