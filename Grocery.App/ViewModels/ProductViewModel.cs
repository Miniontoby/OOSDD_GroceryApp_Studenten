using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    public partial class ProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        public GlobalViewModel _global { get; set; }

        public ObservableCollection<Product> Products { get; set; }

        public ProductViewModel(IProductService productService, GlobalViewModel global)
        {
            _productService = productService;
            _global = global;
            Products = [];
        }

        [RelayCommand]
        public async Task ShowNewProduct()
        {
            if (_global.Client?.Role == Role.Admin) await Shell.Current.GoToAsync(nameof(Views.NewProductView), true);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            Products.Clear();
            foreach (Product product in _productService.GetAll())
                Products.Add(product);
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Products.Clear();
        }
    }
}
