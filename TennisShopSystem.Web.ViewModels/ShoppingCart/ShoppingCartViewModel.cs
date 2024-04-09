namespace TennisShopSystem.Web.ViewModels.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Security.AccessControl;

    using TennisShopSystem.Data.Models;

    public class ShoppingCartViewModel
    {
        public decimal? TotalPrice { get; set; }

        public int? TotalQuantity { get; set; }

        public List<ShoppingCartItem> CartItems { get; set; } = new();
    }
}
