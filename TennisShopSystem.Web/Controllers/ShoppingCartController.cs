namespace TennisShopSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TennisShopSystem.Data;
    using TennisShopSystem.Data.Models;
    using TennisShopSystem.Web.ViewModels.ShoppingCart;
    using static TennisShopSystem.Web.Infrastructure.Extensions.SessionExtensions;
    using Services.Data.Models.Product;

    //TODO: Authorize, AllowAnonymous;

    public class ShoppingCartController : Controller
    {
        private readonly TennisShopDbContext context;
        private List<ShoppingCartItem> cartItems;

        public ShoppingCartController(TennisShopDbContext context)
        {
            this.context = context;
            cartItems = new List<ShoppingCartItem>();
        }

        public IActionResult AddToCart(string id)
        {
            var productToAdd = context.Products.Find(Guid.Parse(id));

            var currentCartItems = HttpContext.Session
                .Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var existingItem = currentCartItems
                .FirstOrDefault(ci => ci.Product.Id.ToString() == id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                currentCartItems.Add(new ShoppingCartItem()
                {
                    Product = productToAdd,
                    Quantity = 1
                });
            }

            HttpContext.Session.Set("Cart", currentCartItems);

            return RedirectToAction("ViewCart");
        }

        public IActionResult ViewCart()
        {
            var currentCartItems = HttpContext.Session
                .Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var cartViewModel = new ShoppingCartViewModel()
            {
                CartItems = currentCartItems,
                TotalPrice = currentCartItems.Sum(item => item.Product.Price * item.Quantity)
            };

            return this.View(cartViewModel);
        }

        public IActionResult DecreaseItemQuantity(string id)
        {
            var currentCartItems =
                HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var itemToRemove = currentCartItems.FirstOrDefault(item => item.Product.Id.ToString() == id);

            if (itemToRemove != null)
            {
                if (itemToRemove.Quantity > 1)
                {
                    itemToRemove.Quantity--;
                }
                else
                {
                    currentCartItems.Remove(itemToRemove);
                }
            }

            HttpContext.Session.Set("Cart", currentCartItems);

            return RedirectToAction("ViewCart");
        }

        public IActionResult IncreaseItemQuantity(string id)
        {
            var currentCartItems =
                HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var itemToUpdate =
                currentCartItems.FirstOrDefault(i => i.Product.Id.ToString() == id);

            itemToUpdate.Quantity++;

            var cartViewModel = new ShoppingCartViewModel()
            {
                CartItems = currentCartItems,
                TotalPrice = currentCartItems.Sum(item => item.Product.Price * item.Quantity)
            };

            HttpContext.Session.Set("Cart", currentCartItems);

            return RedirectToAction("ViewCart", cartViewModel);
        }

        public IActionResult RemoveItem(string id)
        {
            var currentCartItems =
                HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var itemToRemove = currentCartItems.FirstOrDefault(item => item.Product.Id.ToString() == id);

            if (itemToRemove != null)
            {
                currentCartItems.Remove(itemToRemove);
            }

            HttpContext.Session.Set("Cart", currentCartItems);

            return RedirectToAction("ViewCart");
        }
    }
}
