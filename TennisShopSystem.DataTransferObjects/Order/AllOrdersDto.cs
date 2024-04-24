namespace TennisShopSystem.DataTransferObjects.Order
{
    public class AllOrdersDto
    {
        public List<OrderDetailsDto> Orders { get; set; } = new();
    }
}
