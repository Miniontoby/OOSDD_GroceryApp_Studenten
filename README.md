# GroceryApp - Studentversie  

Huidige versie status: bevat Sprint 1, Sprint 2, Sprint 3, Sprint 4 en Sprint 5

Repo link: <https://github.com/Miniontoby/OOSDD_GroceryApp_Studenten>

[![MAUI Unit Tests](https://github.com/Miniontoby/OOSDD_GroceryApp_Studenten/actions/workflows/maui-tests.yml/badge.svg)](https://github.com/Miniontoby/OOSDD_GroceryApp_Studenten/actions/workflows/maui-tests.yml)

## Studentversie:  

### UC01 Tonen boodschappenlijsten  
Is compleet.  

### UC02 Tonen inhoud boodschappenlijst  
Is compleet.  
~~In het bestand `GroceryListItem.cs` uit het project Grocery.Core.Models:~~
- ~~De member variabelen wijzigen in properties~~
- ~~In de constructor de doorgegeven waarden koppelen aan de properties.~~

### UC03 Tonen producten  
Is compleet.  
~~In het bestand `ProductRepository.cs` uit het project Grocery.Core.Data:~~
- ~~Initieer in de constructor de lijst met 4 nieuwe producten:~~
  - ~~Melk[voorraad 300]~~
  - ~~Kaas[voorraad 100]~~
  - ~~Brood[voorraad 400]~~
  - ~~Cornflakes[voorraad 0]~~
- ~~In de methode GetAll() zorg je dat de lijst met producten wordt meegegeven.~~

----

### UC04 Kiezen kleur boodschappenlijst  
Is compleet.

### UC05 Product op boodschappenlijst plaatsen:  
Is compleet. 
- ~~`GetAvailableProducts()`~~  
	~~De header van de functie bestaat maar de inhoud niet.~~  
	~~Zorg dat je een lijst maakt met de beschikbare producten (dit zijn producten waarvan nog voorraad bestaat en die niet al op de boodschappenlijst staat).~~  
- ~~`AddProduct()`~~   
	~~Zorg dat het gekozen beschikbare product op de boodschappenlijst komt (door middel van de GroceryListItemsService).~~  

Bevat bug met viewmodel caching... Als je producten page opent, en daarna terug naar lijstje en dan een product toevoegd, dan blijft producten page zelfde aantal tonen
Zelfde als je eerst een product toevoegt, dan naar producten en dan weer een ander product aan een lijstje toevoegt en dan weer terug naar producten

### UC06 Inloggen  
Is compleet.

~~Een collega is ziek maar heeft al een deel van de inlogfunctionaliteit gemaakt.~~  
~~Dit betreft het Loginscherm (LoginView) met bijbehorend ViewModel (LoginViewModel),~~  
~~maar ook al een deel van de authenticatieService (AuthService in Grocery.Core),~~  
~~de clientrepository (ClientRepository in Grocery.Core.Data)~~  
~~en de client class (Client in Grocery.Core).~~  
~~De opdracht is om zelfstandig de login functionaliteit te laten werken.~~  

~~*Stappenplan:*~~  
1. ~~Begin met de Client class en zorg dat er gebruik wordt gemaakt van Properties.~~  
2. ~~In de ClientRepository wordt nu steeds een vaste client uit de lijst geretourneerd. Werk dit uit zodat de juiste Client wordt geretourneerd.~~  
3. ~~Werk de klasse AuthService verder uit, zodat daadwerkelijk de controle op het ingevoerde password wordt uitgevoerd.~~  
4. ~~Zorg dat de LoginView.xaml wordt toegevoegd aan het Grocery.App project in de Views folder (Add ExistingItem). De file bevindt zich al op deze plek, maar wordt nu niet meegecompileerd.~~  
5. ~~In MauiProgram class van de Grocery.App staan de registraties van de AuthService en de LoginView in comment --> haal de // weg.~~  
6. ~~In App.xaml.cs staat /*LoginViewModel viewModel*/ haal hier /* en */ weg, zodat het LoginViewModel beschikbaar komt.~~  
7. ~~In App.xaml.cs staat //MainPage = new LoginView(viewModel); Haal hier de // weg en zet de regel erboven in commentaar, zodat AppShell wordt uitgeschakeld.~~  
8. ~~Uncomment de route naar het Login scherm in AppShell.xaml.cs: //Routing.RegisterRoute("Login", typeof(LoginView));~~  

----

## UC07 Delen boodschappenlijst  
Is compleet.  
  
## UC08 Zoeken producten  
Is compleet.  

~~Aanvullen:~~  
- ~~In de GroceryListItemsView zitten twee Collection Views, namelijk één voor de inhoud van de boodschappenlijst en één voor producten die je toe kunt voegen aan de boodschappenlijst~~  
- ~~Voeg boven de tweede CollectionView een zoekveld (SearchBar) in om op producten te kunnen zoeken.~~  
- ~~Zorg dat de SearchCommand wordt gebonden aan een functie in het onderliggende ViewModel (GroceryListItemsViewModel) en dat de zoekterm die in het zoekveld is ingetypt gebruikt wordt als parameter (SearchCommandParameter).~~  
- ~~Werk in het viewModel (GroceryListItemsViewModel) de zoekfunctie uit en zorg dat de beschikbare producten worden gefilterd op de zoekterm!~~  

## UC09 Registratie gebruiker  
Is compleet.  
~~Of een ander idee zelf uitwerken. Dit betekent ook dat de documentatie hiervoor ontwikkeld moet worden.~~  

----

## UC10 Productaantal in boodschappenlijst  
Is compleet.  

## UC11 Meest verkochte producten
Vereist aanvulling:  
- ~~Werk in GroceryListItemsService de methode GetBestSellingProducts uit.~~  
- ~~In BestSellingProductsView de kop van de tabel aanvullen met de gewenste kopregel boven de tabel. Daarnaast de inhoud van de tabel uitwerken.~~  

## UC13 Klanten tonen per product  
Deze UC toont de klanten die een bepaald product hebben gekocht:  
- ~~Maak enum Role met als waarden None en Admin.~~  
- ~~Geef de Client class een property Role met als type de enum Role. De default waarde is None.~~  
- ~~In Client Repo koppel je de rol Role.Admin aan user3 (= admin).~~  
- ~~In BoughtProductsService werk je de Get(productid) functie uit zodat alle Clients die product met productid hebben gekocht met client, boodschappenlijst en product in de lijst staan die wordt geretourneerd.~~  
- ~~In BoughtProductsView moet de naam van de Client en de naam van de Boodschappenlijst worden getoond in de CollectionView.~~  
- ~~In BoughtProductsViewModel de OnSelectedProductChanged uitwerken zodat bij een ander product de lijst correct wordt gevuld.~~  
- ~~In GroceryListViewModel maak je de methode ShowBoughtProducts(). Als de Client de rol admin heeft dan navigeer je naar BoughtProductsView. Anders doe je niets.~~  
- ~~In GroceryListView voeg je een ToolbarItem toe met als binding Client.Name en als Command ShowBoughtProducts.~~  


----

## UC15 Toevoegen THT datum aan product  
Is compleet.  

## UC14 Toevoegen prijzen:  
- Prijs toevoegen aan product class en uitbreiden constructor chain.  
- ProductRepository --> prijsveld vullen met waarden.  
- ProductView uitbreiden met kolom voor de prijs (header en inhoud van de tabel).      

## UC12 Productcategoriën toevoegen --> zelfstandig uitwerken:  
Ontwerp:
>```mermaid
>classDiagram
>direction LR
>    class Product {
>	    +int Id
>	    +string Name
>	    +int Stock
>	    +DateOnly ShelfLife
>	    +Decimal Price
>   }
>    class ProductCategory {
>	    +int Id
>	    +string Name
>	    +int ProductId
>	    +int CategoryId
>    }
>    class Category {
>	    +int Id
>	    +string Name
>    }
>
>    Product "1" -- "*" ProductCategory
>    ProductCategory "*" -- "1" Category
> ```
Stappenplan:  
- Maak class Category  
- Maak class ProductCategory  
- Maak Interface en Repository voor Category  
- Maak Interface en Repository voor ProductCategory  
- Maak Interface en Service voor Category  
- Maak Interface en Service voor ProductCategory  
- Registreer de gemaakte Repo's en services in MauiProgramm  
- Maak CategoriesViewModel.  
- Maak CategoriesView.  
- Registreer De view en het ViewModel in MauiProgramm.  
- Maak een menu entry in de tabbar in AppShell.xaml en registreer route in AppShell.xaml.cs  
- Maak ProductCategoriesViewModel.  
- Maak ProductCategoriesView.  
- Registreer De view en het ViewModel in MauiProgramm.  
- Zorg dat de ProductCategoriesView gestart kan worden na het klikken op een Category in CategoriesView  
- Registreer route naar ProductCategoriesView in AppShell.xaml.cs  


