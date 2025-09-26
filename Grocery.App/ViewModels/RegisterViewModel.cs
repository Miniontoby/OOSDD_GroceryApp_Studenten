
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.App.ViewModels
{
    public partial class RegisterViewModel(IAuthService authService, GlobalViewModel global) : BaseViewModel
    {
        private readonly IAuthService _authService = authService;
        private readonly GlobalViewModel _global = global;

        [ObservableProperty]
        private string name = "";

        [ObservableProperty]
        private string email = "";

        [ObservableProperty]
        private string password = "";

        [ObservableProperty]
        private string registerMessage = "";

        [RelayCommand]
        private void Register()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                RegisterMessage = "Ongeldige registratie gegevens.";
            }
            else
            {
                Client? authenticatedClient = _authService.Register(Name, Email, Password);
                if (authenticatedClient != null)
                {
                    RegisterMessage = $"Welkom {authenticatedClient.Name}!";
                    _global.Client = authenticatedClient;
                    if (Application.Current?.MainPage is not null)
                        Application.Current.MainPage = new AppShell();
                }
                else
                {
                    RegisterMessage = "Account bestaat al met deze gegevens.";
                }
            }
        }

        [RelayCommand]
        private void GoToLogin()
        {
            if (Application.Current?.MainPage is not null)
                Application.Current.MainPage = new LoginView(new LoginViewModel(_authService, _global));
        }
    }
}
