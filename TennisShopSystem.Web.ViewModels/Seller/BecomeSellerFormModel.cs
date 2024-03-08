namespace TennisShopSystem.Web.ViewModels.Seller
{
    using System.ComponentModel.DataAnnotations;
    using static TennisShopSystem.Common.EntityValidationConstants.Seller;

    public class BecomeSellerFormModel
    {
        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Phone]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;
    }
}
