namespace TennisShopSystem.Web.ViewModels.OrderDetails
{
    using System.Collections.Generic;
    using Data.Models;
    using TennisShopSystem.Web.ViewModels.Brand;
    using TennisShopSystem.Web.ViewModels.Category;

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

        public IEnumerable<ProductSelectCategoryFormModel> Categories { get; set; } 
            = new List<ProductSelectCategoryFormModel>();

        public IEnumerable<ProductSelectBrandFormModel> Brands { get; set; } 
            = new List<ProductSelectBrandFormModel>();
    }
}
