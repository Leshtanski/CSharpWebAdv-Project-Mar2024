namespace TennisShopSystem.Services.Data.Interfaces
{
    using Web.ViewModels.Brand;

    public interface IBrandService
    {
        Task<IEnumerable<ProductSelectBrandFormModel>> AllBrandsAsync();

        Task<bool> ExistsByIdAsync(int id);

        Task<IEnumerable<string>> AllBrandNamesAsync();
    }
}
