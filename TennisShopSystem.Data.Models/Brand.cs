namespace TennisShopSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static TennisShopSystem.Common.EntityValidationConstants.Brand;

    public class Brand
    {
        public Brand()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
