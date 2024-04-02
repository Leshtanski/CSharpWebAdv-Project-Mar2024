namespace TennisShopSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using TennisShopSystem.Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using TennisShopSystem.Data.Models;
    using TennisShopSystem.Web.Infrastructure.Extensions;
    using TennisShopSystem.Web.ViewModels.OrderDetails;
    using Microsoft.EntityFrameworkCore;

    using static TennisShopSystem.Common.NotificationMessagesConstants;

    [Authorize]
    public class OrderController : Controller
    {
        //TODO: Create OrderService and here in the methods check is current User allowed to purchase and User.IsAdmin()?

        private readonly TennisShopDbContext dbContext;

        public OrderController(TennisShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult ConfirmPurchase()
        {
            var currentCartItems = HttpContext.Session
                .Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            OrderDetailsFormModel model = new()
            {
                TotalPrice = 0,
                Items = currentCartItems
            };

            foreach (var item in currentCartItems)
            {
                model.TotalPrice += item.ItemQuantity * item.Product.Price;
            }

            HttpContext.Session.Set("Cart", currentCartItems);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPurchase(OrderDetailsFormModel model)
        {
            string userId = this.User.GetId()!;

            var currentCartItems = HttpContext.Session
                .Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            model.Items = currentCartItems;

            decimal totalPrice = 0;

            foreach (var item in model.Items)
            {
                totalPrice += item.ItemQuantity * item.Product.Price;
            }

            OrderDetails orderDetails = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                EmailAddress = model.EmailAddress,
                Comment = model.Comment,
                TotalPrice = totalPrice,
                OrderedOn = DateTime.UtcNow
            };

            Order order = new()
            {
                UserId = Guid.Parse(userId),
                OrderDetailsId = orderDetails.Id
            };

            foreach (var item in model.Items)
            {
                OrderedItem orderedItem = new()
                {
                    ProductId = item.Product.Id.ToString(),
                    OrderedQuantity = item.ItemQuantity,
                    OrderDetailsId = orderDetails.Id
                };

                await this.dbContext.OrderedItems.AddAsync(orderedItem);
            }

            await this.dbContext.OrdersDetails.AddAsync(orderDetails);
            await this.dbContext.Orders.AddAsync(order);

            foreach (var item in currentCartItems)
            {
                Product productToDecreaseQuantity = await this.dbContext
                    .Products
                    .FirstAsync(p => p.Id == item.Product.Id);

                if (productToDecreaseQuantity.AvailableQuantity - item.ItemQuantity < 0)
                {
                    return this.GeneralError();
                }

                productToDecreaseQuantity.AvailableQuantity -= item.ItemQuantity;

                if (productToDecreaseQuantity.AvailableQuantity == 0)
                {
                    productToDecreaseQuantity.IsAvailable = false;
                }
            }

            await this.dbContext.SaveChangesAsync();

            model.OrderId = order.Id;
            model.TotalPrice = totalPrice;

            HttpContext.Session.Set("Cart", currentCartItems);

            return RedirectToAction("CurrentOrderDetails", "OrderDetails", model);
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = "An unexpected error occurred! Please try again later or contact administrator!";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
