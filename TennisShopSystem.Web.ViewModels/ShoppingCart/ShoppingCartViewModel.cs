namespace TennisShopSystem.Web.ViewModels.ShoppingCart
{
    using System.Collections.Generic;

    using Data.Models;
    using TennisShopSystem.Web.ViewModels.Brand;
    using TennisShopSystem.Web.ViewModels.Category;

    public class ShoppingCartViewModel
    {
        public decimal? TotalPrice { get; set; }

        public int? TotalQuantity { get; set; }

        public List<ShoppingCartItem> CartItems { get; set; } = new();

        public IEnumerable<ProductSelectCategoryFormModel> Categories { get; set; }
            = new List<ProductSelectCategoryFormModel>();

        public IEnumerable<ProductSelectBrandFormModel> Brands { get; set; }
            = new List<ProductSelectBrandFormModel>();
    }
}
