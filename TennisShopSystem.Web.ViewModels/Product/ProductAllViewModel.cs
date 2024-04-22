namespace TennisShopSystem.Web.ViewModels.Product
{
    using System.ComponentModel.DataAnnotations;
    using TennisShopSystem.Data.Models;

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

        public bool IsAvailable { get; set; }

        public int? SoldItems { get; set; }
    }
}
