﻿namespace Grocery.Core.Models
{
    public class ProductCategory : Model
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public ProductCategory(int id, int productId, int categoryId) : base(id, "")
        {
            ProductId = productId;
            CategoryId = categoryId;
        }

        public Product Product { get; set; } = new(0, "None", 0, 0.00m);
    }
}
