namespace TennisShopSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Mvc;

    using Data;
    using Data.Models;
    using Infrastructure.Extensions;
    using ViewModels.OrderDetails;
    using TennisShopSystem.Services.Data.Interfaces;

    using static Common.NotificationMessagesConstants;
    

    [Authorize]
    public class OrderController : Controller
    {
        private readonly TennisShopDbContext dbContext;
        private readonly ISellerService sellerService;

        public OrderController(TennisShopDbContext dbContext, ISellerService sellerService)
        {
            this.dbContext = dbContext;
            this.sellerService = sellerService;
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmPurchase()
        {
            string userId = this.User.GetId()!;

            bool isSeller = await sellerService.SellerExistByUserIdAsync(userId);

            if (isSeller && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must not be a seller in order to purchase products!";

                return this.RedirectToAction("Index", "Home");
            }

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

            bool isSeller = await sellerService.SellerExistByUserIdAsync(userId);

            if (isSeller && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must not be a seller in order to purchase products!";

                return this.RedirectToAction("Index", "Home");
            }

            var currentCartItems = HttpContext.Session
                .Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            model.Items = currentCartItems;

            foreach (var item in model.Items)
            {
                model.TotalPrice += item.ItemQuantity * item.Product.Price;
            }

            OrderDetails orderDetails = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                EmailAddress = model.EmailAddress,
                Comment = model.Comment,
                TotalPrice = model.TotalPrice,
                OrderedOn = DateTime.UtcNow
            };

            Order order = new()
            {
                UserId = Guid.Parse(userId),
                OrderDetailsId = orderDetails.Id
            };

            List<OrderedItem> orderedItems = new();

            foreach (var item in model.Items)
            {
                OrderedItem orderedItem = new()
                {
                    ProductId = item.Product.Id.ToString(),
                    OrderedQuantity = item.ItemQuantity,
                    OrderDetailsId = orderDetails.Id
                };

                orderedItems.Add(orderedItem);
            }

            await this.dbContext.OrderedItems.AddRangeAsync(orderedItems);
            await this.dbContext.OrdersDetails.AddAsync(orderDetails);
            await this.dbContext.Orders.AddAsync(order);

            foreach (var item in currentCartItems)
            {
                Product productToDecreaseQuantity = await this.dbContext
                    .Products
                    .FirstAsync(p => p.Id == item.Product.Id);

                if (productToDecreaseQuantity.AvailableQuantity - item.ItemQuantity < 0)
                {
                    this.TempData[ErrorMessage] = 
                        $"You are trying to purchase more copies of {productToDecreaseQuantity.Title} than available!";

                    HttpContext.Session.Set("Cart", currentCartItems);
                    return RedirectToAction("ViewCart", "ShoppingCart");
                }

                try
                {
                    productToDecreaseQuantity.AvailableQuantity -= item.ItemQuantity;

                    if (productToDecreaseQuantity.AvailableQuantity == 0)
                    {
                        productToDecreaseQuantity.IsAvailable = false;
                    }
                }
                catch (Exception)
                {
                    HttpContext.Session.Set("Cart", currentCartItems);
                    return this.GeneralError();
                }
            }

            await this.dbContext.SaveChangesAsync();

            model.OrderId = order.Id;

            HttpContext.Session.Set("Cart", new List<ShoppingCartItem>());

            return RedirectToAction("CurrentOrderDetails", "OrderDetails", model);
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = "An unexpected error occurred! Please try again later or contact administrator!";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
