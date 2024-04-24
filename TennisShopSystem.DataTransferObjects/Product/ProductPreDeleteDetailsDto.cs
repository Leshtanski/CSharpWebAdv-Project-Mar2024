namespace TennisShopSystem.DataTransferObjects.Product
{
    using System.ComponentModel.DataAnnotations;

    public class ProductPreDeleteDetailsDto
    {
        public string Title { get; set; } = null!;
        
        public string ImageUrl { get; set; } = null!;
    }
}
