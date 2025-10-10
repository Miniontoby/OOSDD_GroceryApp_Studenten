using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Grocery.App.ViewModels
{
    [QueryProperty(nameof(GroceryList), nameof(GroceryList))]
    public partial class GroceryListItemsViewModel : BaseViewModel
    {
        private readonly IGroceryListItemsService _groceryListItemsService;
        private readonly IProductService _productService;
        private readonly IFileSaverService _fileSaverService;
        private string searchText = "";

        public ObservableCollection<GroceryListItem> MyGroceryListItems { get; set; } = [];
        public ObservableCollection<Product> AvailableProducts { get; set; } = [];

        [ObservableProperty]
        GroceryList groceryList = new(0, "None", DateOnly.MinValue, "", 0);
        [ObservableProperty]
        string myMessage = "";

        public GroceryListItemsViewModel(IGroceryListItemsService groceryListItemsService, IProductService productService, IFileSaverService fileSaverService)
        {
            _groceryListItemsService = groceryListItemsService;
            _productService = productService;
            _fileSaverService = fileSaverService;
            Load(groceryList.Id);
        }

        private void Load(int id)
        {
            MyGroceryListItems.Clear();
            foreach (var item in _groceryListItemsService.GetAllOnGroceryListId(id)) MyGroceryListItems.Add(item);
            GetAvailableProducts();
        }

        private void GetAvailableProducts()
        {
            //Maak de lijst AvailableProducts leeg
            AvailableProducts.Clear();

            List<int> groceryProductIdList = [];
            foreach (var item in MyGroceryListItems)
                groceryProductIdList.Add(item.ProductId);
            
            //Haal de lijst met producten op
            foreach (Product product in _productService.GetAll())
            {
                //Houdt rekening met de voorraad (als die nul is kun je het niet meer aanbieden)
                if (product.Stock > 0)
                {
                    //Controleer of het product al op de boodschappenlijst staat, zo niet zet het in de AvailableProducts lijst
                    if (!groceryProductIdList.Contains(product.Id))
                    {
                        if (string.IsNullOrEmpty(searchText) || product.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                            AvailableProducts.Add(product);
                    }
                }
            }
        }

        partial void OnGroceryListChanged(GroceryList value)
        {
            Load(value.Id);
        }

        [RelayCommand]
        public async Task ChangeColor()
        {
            Dictionary<string, object> parameter = new() { { nameof(GroceryList), GroceryList } };
            await Shell.Current.GoToAsync($"{nameof(ChangeColorView)}?Name={GroceryList.Name}", true, parameter);
        }

        [RelayCommand]
        public void AddProduct(Product product)
        {
            //Controleer of het product bestaat en dat de Id > 0
            if (product is null || product.Id <= 0) return;

            // Laat producten die niet beschikbaar zijn, niet toevoegbaar maken
            if (product.Stock <= 0) return;

            //Maak een GroceryListItem met Id 0 en vul de juiste productid en grocerylistid
            GroceryListItem listItem = new(0, GroceryList.Id, product.Id, 1);

            //Voeg het GroceryListItem toe aan de dataset middels de _groceryListItemsService
            _groceryListItemsService.Add(listItem);

            //Werk de voorraad (Stock) van het product bij en zorg dat deze wordt vastgelegd (middels _productService)
            product.Stock -= listItem.Amount;
            _productService.Update(product);

            //Werk de lijst AvailableProducts bij, want dit product is niet meer beschikbaar
            AvailableProducts.Remove(product);

            //call OnGroceryListChanged(GroceryList);
            OnGroceryListChanged(GroceryList);
        }

        [RelayCommand]
        public async Task ShareGroceryList(CancellationToken cancellationToken)
        {
            if (GroceryList is null || MyGroceryListItems is null) return;
            string jsonString = JsonSerializer.Serialize(MyGroceryListItems);
            try
            {
                await _fileSaverService.SaveFileAsync("Boodschappen.json", jsonString, cancellationToken);
                await Toast.Make("Boodschappenlijst is opgeslagen.").Show(cancellationToken);
            }
            catch (Exception ex)
            {
                await Toast.Make($"Opslaan mislukt: {ex.Message}").Show(cancellationToken);
            }
        }

        [RelayCommand]
        public void PerformSearch(string searchText)
        {
            if (GroceryList is null || MyGroceryListItems is null) return;
            this.searchText = searchText;
            GetAvailableProducts();
        }

        [RelayCommand]
        public void IncreaseAmount(int productId)
        {
            if (GroceryList is null || MyGroceryListItems is null) return;
            GroceryListItem? item = MyGroceryListItems.FirstOrDefault(x => x.ProductId == productId);
            if (item is null) return;
            if (item.Amount >= item.Product.Stock) return;
            item.Amount++;
            _groceryListItemsService.Update(item);
            item.Product.Stock--;
            _productService.Update(item.Product);
            OnGroceryListChanged(GroceryList);
        }

        [RelayCommand]
        public void DecreaseAmount(int productId)
        {
            GroceryListItem? item = MyGroceryListItems.FirstOrDefault(x => x.ProductId == productId);
            if (item is null) return;
            if (item.Amount <= 1)
            {
                item.Amount = 0;
                _groceryListItemsService.Delete(item);
                AvailableProducts.Add(item.Product);
                OnGroceryListChanged(GroceryList);
                return;
            }
            item.Amount--;
            _groceryListItemsService.Update(item);
            item.Product.Stock++;
            _productService.Update(item.Product);
            OnGroceryListChanged(GroceryList);
        }
    }
}
