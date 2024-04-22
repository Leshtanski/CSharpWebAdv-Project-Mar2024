namespace TennisShopSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using ViewModels.ShoppingCart;

    using static TennisShopSystem.Web.Infrastructure.Extensions.SessionExtensions;
    using TennisShopSystem.Services.Data.Interfaces;

    public class ShoppingCartController : Controller
    {
        private readonly TennisShopDbContext dbContext;
        private readonly IBrandService brandService;
        private readonly ICategoryService categoryService;

        public ShoppingCartController(TennisShopDbContext context, ICategoryService categoryService, IBrandService brandService)
        {
            this.dbContext = context;
            this.categoryService = categoryService;
            this.brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> AddToCart(string id)
        {
            var productToAdd = await this.dbContext
                .Products
                .FirstOrDefaultAsync(p => p.Id.ToString() == id);

            if (productToAdd == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var currentCartItems = HttpContext.Session
                .Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var existingItem = currentCartItems
                .FirstOrDefault(ci => ci.Product.Id.ToString() == id.ToLower());

            if (existingItem != null)
            {
                if (existingItem.Product.AvailableQuantity == existingItem.ItemQuantity)
                {
                    this.TempData["WarningMessage"] = "No more copies of the item to add!";
                    return this.RedirectToAction("All", "Product");
                }

                existingItem.ItemQuantity++;
                this.TempData["SuccessMessage"] = "Another copy of the item successfully added to shopping cart!";
            }
            else
            {
                currentCartItems.Add(new ShoppingCartItem()
                {
                    Product = productToAdd!,
                    ItemQuantity = 1
                });

                this.TempData["SuccessMessage"] = "Item successfully added to shopping cart!";
            }

            HttpContext.Session.Set("Cart", currentCartItems);

            return this.RedirectToAction("All", "Product");
        }
        
        public async Task<IActionResult> ViewCart()
        {
            var currentCartItems = HttpContext.Session
                .Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var cartViewModel = new ShoppingCartViewModel()
            {
                CartItems = currentCartItems,
                TotalPrice = currentCartItems.Sum(item => item.Product.Price * item.ItemQuantity),
                Brands = await this.brandService.AllBrandsAsync(),
                Categories = await this.categoryService.AllCategoriesAsync()
            };

            return this.View(cartViewModel);
        }
        
        public IActionResult DecreaseItemQuantity(string id)
        {
            var currentCartItems =
                HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var itemToRemove = currentCartItems
                .FirstOrDefault(item => item.Product.Id.ToString() == id);

            if (itemToRemove != null)
            {
                if (itemToRemove.ItemQuantity > 1)
                {
                    itemToRemove.ItemQuantity--;
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

            if (itemToUpdate!.Product.AvailableQuantity == itemToUpdate.ItemQuantity)
            {
                this.TempData["WarningMessage"] = "No more copies of the item to add!";
            }
            else
            {
                itemToUpdate.ItemQuantity++;
                this.TempData["SuccessMessage"] = "Successfully added one more copy to the item quantity!";
            }

            HttpContext.Session.Set("Cart", currentCartItems);

            return RedirectToAction("ViewCart");
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
