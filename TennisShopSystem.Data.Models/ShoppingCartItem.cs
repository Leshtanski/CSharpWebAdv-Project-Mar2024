﻿namespace TennisShopSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public int ItemQuantity { get; set; }
    }
}
