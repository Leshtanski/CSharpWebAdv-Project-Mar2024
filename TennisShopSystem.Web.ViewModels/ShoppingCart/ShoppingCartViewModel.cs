namespace TennisShopSystem.Web.ViewModels.ShoppingCart
{
    using System.Collections.Generic;

    using Data.Models;

    public class ShoppingCartViewModel
    {
        public decimal? TotalPrice { get; set; }

        public int? TotalQuantity { get; set; }

        public List<ShoppingCartItem> CartItems { get; set; } = new();
    }
}
