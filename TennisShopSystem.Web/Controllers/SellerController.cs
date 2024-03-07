﻿namespace TennisShopSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class SellerController : Controller
    {
        public async Task<IActionResult> Become()
        {
            return View();
        }
    }
}
