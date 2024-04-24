namespace TennisShopSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Data.Models;
    using Infrastructure.Extensions;
    using ViewModels.OrderDetails;
    using TennisShopSystem.Services.Data.Interfaces;

    using static Common.NotificationMessagesConstants;
    using TennisShopSystem.DataTransferObjects.Order;

    [Authorize]
    public class OrderController : Controller
    {
        private readonly ISellerService sellerService;
        private readonly IOrderService orderService;

        public OrderController(ISellerService sellerService, IOrderService orderService)
        {
            this.sellerService = sellerService;
            this.orderService = orderService;
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

            if (currentCartItems.Count == 0)
            {
                this.TempData[ErrorMessage] = "You cannot proceed to complete your purchase because your cart is empty!";
                return this.RedirectToAction("ViewCart", "ShoppingCart");
            }

            OrderDetailsFormModel model = new()
            {
                TotalPrice = 0,
                Items = currentCartItems
            };

            foreach (var item in currentCartItems)
            {
                model.TotalPrice += item.ItemQuantity * item.Product.Price;
            }

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

            OrderDetailsDto orderDto = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                EmailAddress = model.EmailAddress,
                Comment = model.Comment,
                TotalPrice = model.TotalPrice,
                Items = currentCartItems
            };

            string[] result = await this.orderService.CreateAndSaveOrderAsync(orderDto, userId);

            if (result[0] == "Quantity")
            {
                this.TempData[ErrorMessage] =
                    $"You are trying to purchase more copies of {result[1]} than available!";

                HttpContext.Session.Set("Cart", currentCartItems);
                return RedirectToAction("ViewCart", "ShoppingCart");
            }
            else if (result[0] == "Error")
            {
                HttpContext.Session.Set("Cart", currentCartItems);
                return this.GeneralError();
            }
            else if (result[0] == "Success")
            {
                model.OrderId = int.Parse(result[1]);
            }

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
