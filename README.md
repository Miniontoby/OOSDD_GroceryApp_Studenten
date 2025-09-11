# GroceryApp Sprint 1 - Studentversie  
In deze versie zijn de wijzigingen doorgevoerd en is de code compleet.  

## Studentversie:  
**UC01 Tonen boodschappenlijsten**  
Is compleet.

**UC02 Tonen inhoud boodschappenlijst**   
In het bestand `GroceryListItem.cs` uit het project Grocery.Core.Models:
- De member variabelen wijzigen in properties
- In de constructor de doorgegeven waarden koppelen aan de properties.

**UC03 Tonen producten**  
In het bestand `ProductRepository.cs` uit het project Grocery.Core.Data:
- Initieer in de constructor de lijst met 4 nieuwe producten:  
  - Melk[voorraad 300]
  - Kaas[voorraad 100]
  - Brood[voorraad 400]
  - Cornflakes[voorraad 0]
- In de methode GetAll() zorg je dat de lijst met producten wordt meegegeven.
