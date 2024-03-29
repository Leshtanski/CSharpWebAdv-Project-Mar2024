namespace TennisShopSystem.Web.Controllers
{

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using TennisShopSystem.Data;
    using TennisShopSystem.Data.Models;
    using TennisShopSystem.Web.Infrastructure.Extensions;
    using TennisShopSystem.Web.ViewModels.OrderDetails;
    public class OrderDetailsController : Controller
    {
        private readonly TennisShopDbContext dbContext;

        public OrderDetailsController(TennisShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult CurrentOrderDetails(OrderDetailsFormModel formModel)
        {
            var currentCartItems = HttpContext.Session
                .Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            OrderDetailsViewModel model = new()
            {
                Id = formModel.OrderId,
                FirstName = formModel.FirstName,
                LastName = formModel.LastName,
                Address = formModel.Address,
                PhoneNumber = formModel.PhoneNumber,
                EmailAddress = formModel.EmailAddress,
                Comment = formModel.Comment,
                TotalPrice = formModel.TotalPrice,
                Items = currentCartItems
            };

            return View(model);
        }
    }
}
