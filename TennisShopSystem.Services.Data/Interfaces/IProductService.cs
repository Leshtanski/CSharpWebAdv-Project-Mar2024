namespace TennisShopSystem.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TennisShopSystem.Data.Models;
    using TennisShopSystem.DataTransferObjects;
    using TennisShopSystem.DataTransferObjects.Product;

    public interface IProductService
    {
        Task<IEnumerable<IndexModelDto>> LastThreeProductsAsync();

        Task<string> CreateAndReturnIdAsync(ProductFormDto formModel, string sellerId);

        Task<AllProductsFilteredAndPagedDto> AllAsync(AllProductQueryDto queryModel);

        Task<IEnumerable<ProductAllDto>> AllBySellerIdAsync(string sellerId);

        Task<IEnumerable<ProductAllDto>> AllByUserIdAsync(string userId);

        Task<bool> ExistsByIdAsync(string productId);

        Task<ProductDetailsDto> GetDetailsByIdAsync(string productId);

        Task<ProductFormDto> GetProductForEditByIdAsync(string productId);

        Task<bool> IsSellerWithIdOwnerOfProductWithIdAsync(string productId, string sellerId);

        Task EditProductByIdAndFormModelAsync(string productId, ProductFormDto formModel);

        Task<ProductPreDeleteDetailsDto> GetProductForDeleteByIdAsync(string productId);

        Task DeleteProductByIdAsync(string productId);

        Task<bool> ExistsBySellerIdAsync(string productId);

        Task<Product> GetProductByIdAsync(string productId);
    }
}
