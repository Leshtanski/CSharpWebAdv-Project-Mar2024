using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisShopSystem.Data.Models
{
    public class OrderedItem
    {
        public OrderedItem()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public string ProductId { get; set; }

        public int OrderedQuantity { get; set; }

        public Guid OrderDetailsId { get; set; }

        public virtual OrderDetails OrderDetails { get; set; }
    }
}
