﻿namespace TennisShopSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static TennisShopSystem.Common.EntityValidationConstants.Product;

    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public bool IsAvailable { get; set; }
       
        public int AvailableQuantity { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; } = null!;

        public Guid? SellerId { get; set; }

        public virtual Seller? Seller { get; set; }
    }
}
