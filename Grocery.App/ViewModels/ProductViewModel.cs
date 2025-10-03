using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; set; }
        private readonly IProductService _productSerice;

        public ProductViewModel(IProductService productService)
        {
            _productSerice = productService;
            Products = new(_productSerice.GetAll());
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            Products.Clear();
            foreach (Product product in _productSerice.GetAll())
                Products.Add(product);
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Products.Clear();
        }
    }
}
