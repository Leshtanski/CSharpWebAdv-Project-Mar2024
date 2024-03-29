using System.Security.AccessControl;

namespace TennisShopSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static TennisShopSystem.Common.EntityValidationConstants.OrderDetails;

    public class OrderDetails
    {
        public OrderDetails()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(OrderDetailsFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(OrderDetailsLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(OrderDetailsAddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(OrderDetailsPhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [MaxLength(OrderDetailsEmailAddressMaxLength)]
        public string EmailAddress { get; set; } = null!;

        public string? Comment { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public DateTime? OrderedOn { get; set; }

        public virtual List<OrderedItem> OrderedItems { get; set; } = new List<OrderedItem>();
    }
}
