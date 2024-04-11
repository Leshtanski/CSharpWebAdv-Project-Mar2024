namespace TennisShopSystem.Web.ViewModels.Product
{
    using Seller;

    public class ProductDetailsViewModel : ProductAllViewModel
    {
        public string Category { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public SellerInfoOnProductViewModel Seller { get; set; } = null!;
    }
}
