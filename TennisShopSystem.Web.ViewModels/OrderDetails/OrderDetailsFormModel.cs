namespace TennisShopSystem.Web.ViewModels.OrderDetails
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TennisShopSystem.Common;
    using TennisShopSystem.Data.Models;

    using static Common.EntityValidationConstants.OrderDetails;

    public class OrderDetailsFormModel
    {
        [Required]
        [StringLength(OrderDetailsFirstNameMaxLength, MinimumLength = OrderDetailsFirstNameMinLength,
            ErrorMessage = "{0} must be between {2} and {1} symbols long!")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(OrderDetailsLastNameMaxLength, MinimumLength = OrderDetailsLastNameMinLength,
            ErrorMessage = "{0} must be between {2} and {1} symbols long!")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(OrderDetailsAddressMaxLength, MinimumLength = OrderDetailsAddressMinLength,
            ErrorMessage = "{0} must be between {2} and {1} symbols long!")]
        [Display(Name = "Address for Delivery")]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(OrderDetailsPhoneNumberMaxLength, MinimumLength = OrderDetailsPhoneNumberMinLength,
            ErrorMessage = "{0} must be between {2} and {1} symbols long!")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(OrderDetailsEmailAddressMaxLength, MinimumLength = OrderDetailsEmailAddressMinLength,
            ErrorMessage = "{0} must be between {2} and {1} symbols long!")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; } = null!;
        
        public string? Comment { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        public List<ShoppingCartItem> Items { get; set; } = null!;

        public int OrderId { get; set; }
    }
}
