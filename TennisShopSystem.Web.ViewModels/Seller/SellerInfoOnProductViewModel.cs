namespace TennisShopSystem.Web.ViewModels.Seller
{
    using System.ComponentModel.DataAnnotations;

    public class SellerInfoOnProductViewModel
    {
        public string Email { get; set; } = null!;

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;
    }
}
