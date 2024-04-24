namespace TennisShopSystem.Services.Data.Interfaces
{
    using TennisShopSystem.DataTransferObjects.Brand;

    public interface IBrandService
    {
        Task<IEnumerable<ProductSelectBrandFormDto>> AllBrandsAsync();

        Task<bool> ExistsByIdAsync(int id);

        Task<IEnumerable<string>> AllBrandNamesAsync();
    }
}
