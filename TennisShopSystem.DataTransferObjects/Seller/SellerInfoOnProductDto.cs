namespace TennisShopSystem.DataTransferObjects.Seller
{
    using System.ComponentModel.DataAnnotations;

    public class SellerInfoOnProductDto
    {
        public string Email { get; set; } = null!;
        
        public string PhoneNumber { get; set; } = null!;
    }
}
