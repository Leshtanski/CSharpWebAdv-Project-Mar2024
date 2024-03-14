using System.ComponentModel.DataAnnotations;

namespace TennisShopSystem.Web.ViewModels.Product
{
    public class ProductPreDeleteDetailsViewModel
    {
        public string Title { get; set; } = null!;

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;
    }
}
