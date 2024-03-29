using Microsoft.EntityFrameworkCore;

namespace TennisShopSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using TennisShopSystem.Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using TennisShopSystem.Data.Models;
    using TennisShopSystem.Web.Infrastructure.Extensions;
    using TennisShopSystem.Web.ViewModels.OrderDetails;

    [Authorize]
    public class OrderController : Controller
    {
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
        public IActionResult ConfirmPurchase(OrderDetailsFormModel model)
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

                this.dbContext.OrderedItems.Add(orderedItem);
            }

            this.dbContext.OrdersDetails.Add(orderDetails);
            this.dbContext.Orders.Add(order);

            foreach (var item in currentCartItems)
            {
                Product productToDecreaseQuantity = this.dbContext.Products.First(p => p.Id == item.Product.Id);

                if (productToDecreaseQuantity.AvailableQuantity - item.ItemQuantity < 0)
                {
                    return RedirectToAction();
                }

                productToDecreaseQuantity.AvailableQuantity -= item.ItemQuantity;

                if (productToDecreaseQuantity.AvailableQuantity == 0)
                {
                    productToDecreaseQuantity.IsAvailable = false;
                }
            }

            this.dbContext.SaveChanges();

            model.OrderId = order.Id;
            model.TotalPrice = totalPrice;

            HttpContext.Session.Set("Cart", currentCartItems);

            return RedirectToAction("CurrentOrderDetails", "OrderDetails", model);
        }
    }
}
