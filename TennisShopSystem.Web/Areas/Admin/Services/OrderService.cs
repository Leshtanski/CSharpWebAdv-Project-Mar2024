namespace TennisShopSystem.Web.Areas.Admin.Services
{
    using Data;
    using TennisShopSystem.Web.ViewModels.OrderDetails;
    using Interfaces;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class OrderService : IOrderService
    {
        private readonly TennisShopDbContext dbContext;

        public OrderService(TennisShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AllOrdersViewModel> GetAllOrdersAsync()
        {
            List<Order> orders = await this.dbContext.Orders.ToListAsync();
                
            List<OrderDetails> ordersDetails = await this.dbContext.OrdersDetails.ToListAsync();

            List<OrderedItem> orderedItems = await this.dbContext.OrderedItems.ToListAsync();

            AllOrdersViewModel viewModel = new();

            foreach (var order in orders)
            {
                foreach (var orderDetails in ordersDetails.Where(od => od.Id == order.OrderDetailsId))
                {
                    OrderDetailsViewModel model = new()
                    {
                        Id = order.Id,
                        FirstName = orderDetails.FirstName,
                        LastName = orderDetails.LastName,
                        Address = orderDetails.Address,
                        PhoneNumber = orderDetails.PhoneNumber,
                        EmailAddress = orderDetails.EmailAddress,
                        TotalPrice = orderDetails.TotalPrice,
                        Items = new List<ShoppingCartItem>(),
                        OrderRegisteredOn = orderDetails.OrderedOn.ToString()
                    };

                    foreach (var orderedItem in orderedItems.Where(oi => oi.OrderDetailsId == orderDetails.Id))
                    {
                        var productToShoppingCartItem = await this.dbContext
                            .Products
                            .FirstOrDefaultAsync(pts => pts.Id.ToString() == orderedItem.ProductId);

                        ShoppingCartItem shoppingCartItem = new()
                        {
                            Product = productToShoppingCartItem!,
                            ItemQuantity = orderedItem.OrderedQuantity
                        };


                        model.Items.Add(shoppingCartItem);
                    }

                    viewModel.Orders.Add(model);

                }

            }

            return viewModel;
        }
    }
}
