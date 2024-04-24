namespace TennisShopSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TennisShopSystem.DataTransferObjects;
    using TennisShopSystem.Services.Data.Interfaces;
    using TennisShopSystem.Web.ViewModels.Home;
    using static Common.GeneralApplicationConstants;

    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            if (this.User.IsInRole(AdminRoleName))
            {
                return this.RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }

            IEnumerable<IndexModelDto> models = 
                await this.productService.LastThreeProductsAsync();

            List<IndexViewModel> viewModel = new();

            foreach (IndexModelDto modelDto in models)
            {
                IndexViewModel model = new()
                {
                    Id = modelDto.Id,
                    Title = modelDto.Title,
                    ImageUrl = modelDto.ImageUrl
                };

                viewModel.Add(model);
            }

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400 || statusCode == 404)
            {
                return this.View("Error404");
            }

            return this.View();
        }
    }
}
