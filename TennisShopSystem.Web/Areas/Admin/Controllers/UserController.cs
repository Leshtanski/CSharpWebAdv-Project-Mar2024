namespace TennisShopSystem.Web.Areas.Admin.Controllers
{
    using ViewModels.User;
    using Microsoft.AspNetCore.Mvc;
    using TennisShopSystem.Services.Data.Interfaces;

    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Route("User/All")]
        public async Task<IActionResult> All()
        {
            IEnumerable<UserViewModel> viewModel =
                await this.userService.AllAsync();

            return View(viewModel);
        }
    }
}
