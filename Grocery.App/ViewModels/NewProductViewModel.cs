using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.App.ViewModels
{
    public partial class NewProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;

        [ObservableProperty]
        private Product product;

        public DateTime ShelfLifeDateTime
        {
            get
            {
                return Product.ShelfLife.ToDateTime(TimeOnly.MinValue);
            }
            set
            {
                Product.ShelfLife = DateOnly.FromDateTime(value.Date);
            }
        }

        public NewProductViewModel(IProductService productService)
        {
            _productService = productService;
            Product = new(0, "", 0);
        }

        [RelayCommand]
        public async Task NewProduct()
        {
            _productService.Add(Product);
            await Shell.Current.GoToAsync($"{nameof(Views.ProductView)}", true);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            Product = new(0, "", 0, DateOnly.FromDateTime(DateTime.Today));
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Product = new(0, "", 0);
        }
    }
}
