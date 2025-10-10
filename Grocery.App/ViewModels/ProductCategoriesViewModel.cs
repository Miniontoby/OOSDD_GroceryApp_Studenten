using CommunityToolkit.Mvvm.ComponentModel;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    [QueryProperty(nameof(Category), nameof(Category))]
    public partial class ProductCategoriesViewModel : BaseViewModel
    {
        private readonly IProductCategoryService _productCategoryService;

        public ObservableCollection<ProductCategory> MyProductCategories { get; set; } = [];

        [ObservableProperty]
        Category category = new(0, "");

        public ProductCategoriesViewModel(IProductCategoryService productService)
        {
            _productCategoryService = productService;
            if (category.Id == 0) Category = new(3, "Zuivel");
            Load(category.Id);
        }

        private void Load(int id)
        {
            MyProductCategories.Clear();
            foreach (var item in _productCategoryService.GetAllOnCategoryId(id)) MyProductCategories.Add(item);
        }
    }
}
