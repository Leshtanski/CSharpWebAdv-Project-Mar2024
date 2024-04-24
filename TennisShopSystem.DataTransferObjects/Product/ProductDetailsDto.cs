namespace TennisShopSystem.DataTransferObjects.Product
{
    using TennisShopSystem.DataTransferObjects.Seller;

    public class ProductDetailsDto : ProductAllDto
    {
        public string Category { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public SellerInfoOnProductDto Seller { get; set; } = null!;
    }
}
