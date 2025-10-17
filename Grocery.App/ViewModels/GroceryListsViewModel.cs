using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    public partial class GroceryListsViewModel : BaseViewModel
    {
        private readonly IGroceryListService _groceryListService;
        public GlobalViewModel _global { get; set; }

        public ObservableCollection<GroceryList> GroceryLists { get; set; }

        public GroceryListsViewModel(IGroceryListService groceryListService, GlobalViewModel global) 
        {
            Title = "Boodschappenlijst";
            _groceryListService = groceryListService;
            _global = global;
            GroceryLists = new(_groceryListService.GetAll());
        }

        [RelayCommand]
        public async Task SelectGroceryList(GroceryList groceryList)
        {
            Dictionary<string, object> parameter = new() { { nameof(GroceryList), groceryList } };
            await Shell.Current.GoToAsync($"{nameof(Views.GroceryListItemsView)}?Titel={groceryList.Name}", true, parameter);
        }

        [RelayCommand]
        public async Task ShowBoughtProducts()
        {
            // maak methode ShowBoughtProducts(). Als Client rol admin heeft dan navigeer naar BoughtProductsView. Anders doe je niets.
            if (_global.Client?.Role == Role.Admin) await Shell.Current.GoToAsync(nameof(Views.BoughtProductsView), true);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            GroceryLists.Clear();
            foreach (GroceryList groceryList in _groceryListService.GetAll())
                GroceryLists.Add(groceryList);
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            GroceryLists.Clear();
        }
    }
}
