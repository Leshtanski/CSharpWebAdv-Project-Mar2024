﻿namespace TennisShopSystem.Web.ViewModels.Product
{
    public class MyProductsViewModel
    {
        public IEnumerable<ProductAllViewModel> AddedProducts { get; set; } = null!;

        public IEnumerable<ProductAllViewModel> BoughtProducts { get; set; } = null!;
    }
}
