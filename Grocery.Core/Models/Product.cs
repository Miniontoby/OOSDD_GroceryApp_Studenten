using CommunityToolkit.Mvvm.ComponentModel;

namespace Grocery.Core.Models
{
    public partial class Product : Model
    {
        [ObservableProperty]
        public int stock;
        public decimal Prize { get; set; }
        public DateOnly ShelfLife { get; set; }

        public Product(int id, string name, int stock, decimal prize) : this(id, name, stock, prize, default) { }

        public Product(int id, string name, int stock, decimal prize, DateOnly shelfLife) : base(id, name) 
        {
            Stock = stock;
            Prize = prize;
            ShelfLife = shelfLife;
        }
        public override string? ToString()
        {
            return $"{Name} - €{Prize:0.00} - {Stock} op voorraad";
        }
    }
}
