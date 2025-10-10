using CommunityToolkit.Mvvm.ComponentModel;

namespace Grocery.Core.Models
{
    public partial class Product : Model
    {
        [ObservableProperty]
        public int stock;
        public decimal Price { get; set; }
        public DateOnly ShelfLife { get; set; }

        public Product(int id, string name, int stock, decimal price) : this(id, name, stock, price, default) { }

        public Product(int id, string name, int stock, decimal price, DateOnly shelfLife) : base(id, name) 
        {
            Stock = stock;
            Price = price;
            ShelfLife = shelfLife;
        }
        public override string? ToString()
        {
            return $"{Name} - €{Price:0.00} - {Stock} op voorraad";
        }
    }
}
