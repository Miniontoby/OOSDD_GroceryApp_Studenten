using Grocery.App.ViewModels;
using Grocery.Core.Models;

namespace Grocery.App.Views;

public partial class BoughtProductsView : ContentPage
{
    private readonly BoughtProductsViewModel _viewModel;

    public BoughtProductsView(BoughtProductsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            Product? product = picker.SelectedItem as Product;
            if (product is not null)
                _viewModel.NewSelectedProduct(product);
        }
    }
}