using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoriesRepository)
        {
            _categoryRepository = categoriesRepository;
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category? Get(int id)
        {
            return _categoryRepository.Get(id);
        }

        public Category Add(Category item)
        {
            return _categoryRepository.Add(item);
        }

        public Category? Update(Category item)
        {
            return _categoryRepository.Update(item);
        }

        public Category? Delete(Category item)
        {
            return _categoryRepository.Delete(item);
        }
    }
}
