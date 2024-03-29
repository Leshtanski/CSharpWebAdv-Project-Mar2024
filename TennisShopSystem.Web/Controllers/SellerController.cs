﻿namespace TennisShopSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TennisShopSystem.Services.Data.Interfaces;
    using TennisShopSystem.Web.Infrastructure.Extensions;
    using TennisShopSystem.Web.ViewModels.Seller;
    using static TennisShopSystem.Common.NotificationMessagesConstants;

    [Authorize]
    public class SellerController : Controller
    {
        private readonly ISellerService sellerService;

        public SellerController(ISellerService sellerService)
        {
            this.sellerService = sellerService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();

            bool isSeller = await this.sellerService.SellerExistByUserIdAsync(userId);

            if (isSeller)
            {
                TempData[ErrorMessage] = "You already are a seller!";

                return RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeSellerFormModel model)
        {
            string? userId = this.User.GetId();

            bool isSeller = await this.sellerService.SellerExistByUserIdAsync(userId);

            if (isSeller)
            {
                this.TempData[ErrorMessage] = "You already are a seller!";

                return this.RedirectToAction("Index", "Home");
            }

            bool isPhoneNumberTaken =
                await this.sellerService.SellerExistsByPhoneNumberAsync(model.PhoneNumber);
            
            if (isPhoneNumberTaken)
            {
                this.ModelState.AddModelError(nameof(model.PhoneNumber), "Seller with the provided phone number already exists!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            /* bool userHasPurchases = await this.sellerService
                .HasPurchasesByUserIdAsync(userId);

            if (userHasPurchases)
            {
                this.TempData[ErrorMessage] = "You must not have any purchases in order to become a seller!";
                return this.RedirectToAction("Mine", "Product");
            } */

            try
            {
                await this.sellerService.Create(userId, model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] =
                    "An unexpected error occured while registering you as a seller! Please try again later or contact administrator!";

                return this.RedirectToAction("Index", "Home");
            }

            return this.RedirectToAction("All", "Product");
        }
    }
}
