namespace TennisShopSystem.Services.Data.Interfaces
{
    using TennisShopSystem.Web.ViewModels.Brand;

    public interface IBrandService
    {
        public Task<IEnumerable<ProductSelectBrandFormModel>> AllBrandsAsync();
    }
}
