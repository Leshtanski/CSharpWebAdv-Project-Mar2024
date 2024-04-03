namespace TennisShopSystem.Web.ViewModels.Product
{
    using System.ComponentModel.DataAnnotations;
    using TennisShopSystem.Web.ViewModels.Brand;
    using TennisShopSystem.Web.ViewModels.Category;

    using static TennisShopSystem.Common.EntityValidationConstants.Product;

    public class ProductFormModel
    {
        public ProductFormModel()
        {
            this.Brands = new HashSet<ProductSelectBrandFormModel>();
            this.Categories = new HashSet<ProductSelectCategoryFormModel>();
        }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(ImageUrlMaxLength)]
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
        [Display(Name = "Price Per Item")]
        public decimal Price { get; set; }

        [Required]
        [Range(typeof(int), QuantityMinValue, QuantityMaxValue)]
        [Display(Name = "Item Quantity")]
        public int AvailableQuantity { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int BrandId { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<ProductSelectBrandFormModel> Brands { get; set; }

        public IEnumerable<ProductSelectCategoryFormModel> Categories { get; set; }
    }
}
