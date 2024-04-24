namespace TennisShopSystem.DataTransferObjects.Product
{
    public class AllProductsFilteredAndPagedDto
    {
        public AllProductsFilteredAndPagedDto()
        {
            this.Products = new HashSet<ProductAllDto>();
        }

        public int TotalProductsCount { get; set; }

        public IEnumerable<ProductAllDto> Products { get; set; }
    }
}
