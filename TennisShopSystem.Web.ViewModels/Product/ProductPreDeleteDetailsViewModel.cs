namespace TennisShopSystem.Web.ViewModels.Product
{
    using System.ComponentModel.DataAnnotations;

    public class ProductPreDeleteDetailsViewModel
    {
        public string Title { get; set; } = null!;

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;
    }
}
