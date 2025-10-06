using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    public partial class BoughtProductsViewModel : BaseViewModel
    {
        private readonly IBoughtProductsService _boughtProductsService;

        [ObservableProperty]
        Product? selectedProduct;
        public ObservableCollection<BoughtProducts> BoughtProductsList { get; set; } = [];
        public ObservableCollection<Product> Products { get; set; }

        public BoughtProductsViewModel(IBoughtProductsService boughtProductsService, IProductService productService)
        {
            _boughtProductsService = boughtProductsService;
            Products = new(productService.GetAll());
        }

        partial void OnSelectedProductChanged(Product? oldValue, Product? newValue)
        {
            if (newValue is not null)
            {
                //Zorg dat de lijst BoughtProductsList met de gegevens die passen bij het geselecteerde product. 
                BoughtProductsList.Clear();

                List<BoughtProducts> myBoughtProductsList = _boughtProductsService.Get(newValue.Id);
                foreach (BoughtProducts boughtProduct in myBoughtProductsList)
                    BoughtProductsList.Add(boughtProduct);
            }
        }

        [RelayCommand]
        public void NewSelectedProduct(Product product)
        {
            SelectedProduct = product;
        }
    }
}
