namespace TennisShopSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Data.Models;
    using ViewModels.ShoppingCart;

    using static TennisShopSystem.Web.Infrastructure.Extensions.SessionExtensions;
    using static Common.NotificationMessagesConstants;
    using TennisShopSystem.Services.Data.Interfaces;
    using TennisShopSystem.Web.ViewModels.Brand;
    using TennisShopSystem.Web.ViewModels.Category;
    using TennisShopSystem.DataTransferObjects.Brand;
    using TennisShopSystem.DataTransferObjects.Category;
    using TennisShopSystem.Web.Infrastructure.Extensions;

    public class ShoppingCartController : Controller
    {
        private readonly IBrandService brandService;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly ISellerService sellerService;

        public ShoppingCartController(
            ICategoryService categoryService,
            IBrandService brandService,
            IProductService productService,
            ISellerService sellerService)
        {
            this.categoryService = categoryService;
            this.brandService = brandService;
            this.productService = productService;
            this.sellerService = sellerService;
        }
        
        public async Task<IActionResult> AddToCart(string id)
        {
            bool productToAdd = await this.productService.ExistsByIdAsync(id);

            if (productToAdd == false)
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
                    Product = await this.productService.GetProductByIdAsync(id),
                    ItemQuantity = 1
                });

                this.TempData["SuccessMessage"] = "Item successfully added to shopping cart!";
            }

            HttpContext.Session.Set("Cart", currentCartItems);

            return this.RedirectToAction("All", "Product");
        }
        
        public async Task<IActionResult> ViewCart()
        {
            bool isSeller = await sellerService.SellerExistByUserIdAsync(this.User.GetId());

            if (isSeller && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must not be a seller in order to view the shopping cart!";

                return this.RedirectToAction("Index", "Home");
            }

            var currentCartItems = HttpContext.Session
                .Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            IEnumerable<ProductSelectBrandFormDto> brands = await this.brandService.AllBrandsAsync();
            IEnumerable<ProductSelectCategoryFormDto> categories = await this.categoryService.AllCategoriesAsync();

            IEnumerable<ProductSelectBrandFormModel> modelBrands = brands
                .Select(b => new ProductSelectBrandFormModel()
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToArray();

            IEnumerable<ProductSelectCategoryFormModel> modelCategories = categories
                .Select(c => new ProductSelectCategoryFormModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArray();

            var cartViewModel = new ShoppingCartViewModel()
            {
                CartItems = currentCartItems,
                TotalPrice = currentCartItems.Sum(item => item.Product.Price * item.ItemQuantity),
                Brands = modelBrands,
                Categories = modelCategories
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
