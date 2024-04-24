namespace TennisShopSystem.DataTransferObjects.Order
{
    using TennisShopSystem.Data.Models;

    public class OrderDetailsDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;
        
        public string LastName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;

        public string? Comment { get; set; }

        public decimal TotalPrice { get; set; }

        public List<ShoppingCartItem> Items { get; set; } = null!;

        public int OrderId { get; set; }

        public string? OrderRegisteredOn { get; set; }
    }
}
