﻿namespace Basket.API.Entities
{
    public class ShoppingCartitem
    {
        public int Quantity { get; set; }
        public string Color { get; set; } = null!;
        public decimal Price { get; set; }
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
    }
}
