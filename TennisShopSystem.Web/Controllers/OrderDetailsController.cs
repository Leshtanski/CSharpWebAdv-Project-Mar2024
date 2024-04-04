namespace TennisShopSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Infrastructure.Extensions;
    using ViewModels.OrderDetails;
    using TennisShopSystem.Services.Data.Interfaces;
    
    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class OrderDetailsController : Controller
    {
        private readonly TennisShopDbContext dbContext;
        private readonly ISellerService sellerService;

        public OrderDetailsController(TennisShopDbContext dbContext, ISellerService sellerService)
        {
            this.dbContext = dbContext;
            this.sellerService = sellerService;
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

            bool isSeller = await sellerService.SellerExistByUserIdAsync(userId);

            if (isSeller && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must not be a seller to view orders!";

                return this.RedirectToAction("Index", "Home");
            }

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
