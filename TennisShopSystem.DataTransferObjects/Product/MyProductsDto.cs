namespace TennisShopSystem.DataTransferObjects.Product
{
    public class MyProductsDto
    {
        public IEnumerable<ProductAllDto> AddedProducts { get; set; } = null!;

        public IEnumerable<ProductAllDto> BoughtProducts { get; set; } = null!;
    }
}
