using Grocery.App.ViewModels;

namespace Grocery.App.Views;

public partial class CategoriesView : ContentPage
{
	public CategoriesView(CategoriesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is CategoriesViewModel bindingContext)
        {
            bindingContext.OnAppearing();
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is CategoriesViewModel bindingContext)
        {
            bindingContext.OnDisappearing();
        }
    }
}