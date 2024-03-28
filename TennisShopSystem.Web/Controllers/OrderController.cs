using Microsoft.AspNetCore.Mvc;

namespace TennisShopSystem.Web.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult ConfirmPurchase()
        {
            return View();
        }
    }
}
