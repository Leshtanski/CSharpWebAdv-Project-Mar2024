namespace TennisShopSystem.Services.Data.Interfaces
{
    using TennisShopSystem.Web.ViewModels.Brand;

    public interface IBrandService
    {
        Task<IEnumerable<ProductSelectBrandFormModel>> AllBrandsAsync();

        Task<bool> ExistsByIdAsync(int id);
    }
}
