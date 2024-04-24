namespace TennisShopSystem.Web.Areas.Admin.Controllers
{
    using ViewModels.User;
    using Microsoft.AspNetCore.Mvc;
    using TennisShopSystem.Services.Data.Interfaces;
    using TennisShopSystem.DataTransferObjects.User;

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
            IEnumerable<UserDto> users =
                await this.userService.AllAsync();

            IEnumerable<UserViewModel> viewModel = users
                .Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber
                })
                .ToArray();

            return View(viewModel);
        }
    }
}
