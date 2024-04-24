namespace TennisShopSystem.DataTransferObjects.Product
{
    using System.ComponentModel.DataAnnotations;
    using Brand;
    using Category;

    using static TennisShopSystem.Common.EntityValidationConstants.Product;

    public class ProductFormDto
    {
        public ProductFormDto()
        {
            this.Brands = new HashSet<ProductSelectBrandFormDto>();
            this.Categories = new HashSet<ProductSelectCategoryFormDto>();
        }
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public int AvailableQuantity { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int BrandId { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<ProductSelectBrandFormDto> Brands { get; set; }

        public IEnumerable<ProductSelectCategoryFormDto> Categories { get; set; }
    }
}
