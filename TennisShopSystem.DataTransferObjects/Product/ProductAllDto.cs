namespace TennisShopSystem.DataTransferObjects.Product
{
    public class ProductAllDto
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public int AvailableQuantity { get; set; }

        public bool IsAvailable { get; set; }

        public int? SoldItems { get; set; }
    }
}
