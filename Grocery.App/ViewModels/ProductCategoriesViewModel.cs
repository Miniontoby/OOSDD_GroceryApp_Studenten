using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    [QueryProperty(nameof(Category), nameof(Category))]
    public partial class ProductCategoriesViewModel : BaseViewModel
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;
        private string searchText = "";

        public ObservableCollection<ProductCategory> MyProductCategories { get; set; } = [];
        public ObservableCollection<Product> AvailableProducts { get; set; } = [];

        [ObservableProperty]
        Category category = new(0, "");

        public ProductCategoriesViewModel(IProductCategoryService productCategoryService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
            Load(category.Id);
        }

        private void Load(int id)
        {
            MyProductCategories.Clear();
            foreach (var item in _productCategoryService.GetAllOnCategoryId(id)) MyProductCategories.Add(item);
            GetAvailableProducts();
        }

        private void GetAvailableProducts()
        {
            //Maak de lijst AvailableProducts leeg
            AvailableProducts.Clear();

            List<int> groceryProductIdList = [];
            foreach (var item in MyProductCategories)
                groceryProductIdList.Add(item.ProductId);

            //Haal de lijst met producten op
            foreach (Product product in _productService.GetAll())
            {
                //Controleer of het product al op de boodschappenlijst staat, zo niet zet het in de AvailableProducts lijst
                if (!groceryProductIdList.Contains(product.Id))
                {
                    if (string.IsNullOrEmpty(searchText) || product.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                        AvailableProducts.Add(product);
                }
            }
        }

        partial void OnCategoryChanged(Category? oldValue, Category newValue)
        {
            Load(newValue.Id);
        }

        [RelayCommand]
        public void AddProduct(Product product)
        {
            if (product is null || product.Id <= 0) return;
            ProductCategory item = new(0, product.Id, Category.Id)
            {
                Product = product,
                Category = Category
            };
            _productCategoryService.Add(item);
            AvailableProducts.Remove(product);
            OnCategoryChanged(null, Category);
        }

        [RelayCommand]
        public void RemoveProduct(int productCategoryId)
        {
            ProductCategory? item = _productCategoryService.Get(productCategoryId);
            if (item is null || item.Id <= 0) return;
            _productCategoryService.Delete(item);
            Product? product = _productService.Get(item.ProductId);
            if (product is not null) AvailableProducts.Add(product);
            OnCategoryChanged(null, Category);
        }

        [RelayCommand]
        public void PerformSearch(string searchText)
        {
            if (Category is null || MyProductCategories is null) return;
            this.searchText = searchText;
            GetAvailableProducts();
        }
    }
}
