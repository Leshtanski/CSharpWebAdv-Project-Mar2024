using System.ComponentModel.DataAnnotations;

namespace TennisShopSystem.Web.ViewModels.Product
{
    public class ProductAllViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Price per item")]
        public decimal Price { get; set; }

        public int AvailableQuantity { get; set; }
    }
}
