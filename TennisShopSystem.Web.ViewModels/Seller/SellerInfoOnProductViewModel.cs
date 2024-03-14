using System.ComponentModel.DataAnnotations;

namespace TennisShopSystem.Web.ViewModels.Seller
{
    public class SellerInfoOnProductViewModel
    {
        public string Email { get; set; } = null!;

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;
    }
}
