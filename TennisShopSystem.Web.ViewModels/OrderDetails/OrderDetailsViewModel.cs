using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisShopSystem.Data.Models;

namespace TennisShopSystem.Web.ViewModels.OrderDetails
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string? Comment { get; set; }
        
        public decimal TotalPrice { get; set; }

        public List<ShoppingCartItem> Items { get; set; }

        public string? OrderRegisteredOn { get; set; }
    }
}
