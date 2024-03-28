using System.ComponentModel.DataAnnotations;

namespace TennisShopSystem.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        public Guid? OrderDetailsId { get; set; }

        public virtual OrderDetails? OrderDetails { get; set; }
    }
}
