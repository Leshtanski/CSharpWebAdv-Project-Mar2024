namespace TennisShopSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using TennisShopSystem.Data;
    using TennisShopSystem.Data.Models;
    using TennisShopSystem.Web.Infrastructure.Extensions;
    using TennisShopSystem.Web.ViewModels.OrderDetails;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    public class OrderDetailsController : Controller
    {
        private readonly TennisShopDbContext dbContext;

        public OrderDetailsController(TennisShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult CurrentOrderDetails(OrderDetailsFormModel formModel)
        {
            var currentCartItems = HttpContext.Session
                .Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            OrderDetailsViewModel model = new()
            {
                Id = formModel.OrderId,
                FirstName = formModel.FirstName,
                LastName = formModel.LastName,
                Address = formModel.Address,
                PhoneNumber = formModel.PhoneNumber,
                EmailAddress = formModel.EmailAddress,
                Comment = formModel.Comment,
                TotalPrice = formModel.TotalPrice,
                Items = currentCartItems
            };

            return View(model);
        }

        public async Task<IActionResult> MyOrdersDetails()
        {
            string userId = this.User.GetId()!;

            List<Order> orders = await this.dbContext
                .Orders
                .Where(o => o.UserId.ToString() == userId)
                .ToListAsync();

            List<OrderDetails> ordersDetails = new();

            foreach (var order in orders)
            {
                OrderDetails orderDetails = await this.dbContext
                    .OrdersDetails
                    .FirstAsync(od => od.Id == order.OrderDetailsId);

                ordersDetails.Add(orderDetails);
            }

            List<OrderedItem> allOrderedItems = new();

            foreach (var orderDetail in ordersDetails)
            {
                List<OrderedItem> orderedItems = await this.dbContext
                    .OrderedItems
                    .Where(oi => oi.OrderDetailsId == orderDetail.Id)
                    .ToListAsync();

                allOrderedItems.AddRange(orderedItems);
            }

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

                    foreach (var orderedItem in allOrderedItems.Where(oi => oi.OrderDetailsId == orderDetails.Id))
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

            return View(viewModel);
        }
    }
}
