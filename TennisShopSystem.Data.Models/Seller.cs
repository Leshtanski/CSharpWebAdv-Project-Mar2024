﻿namespace TennisShopSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Seller;
    public class Seller
    {
        public Seller()
        {
            this.Id = Guid.NewGuid();
            this.OwnedProducts = new HashSet<Product>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<Product> OwnedProducts { get; set; }
    }
}
