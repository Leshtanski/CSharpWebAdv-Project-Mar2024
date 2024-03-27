namespace TennisShopSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TennisShopSystem.Data;
    using TennisShopSystem.Data.Models;
    using TennisShopSystem.Services.Data.Interfaces;
    using TennisShopSystem.Services.Data.Models.Product;
    using TennisShopSystem.Web.ViewModels.Home;
    using TennisShopSystem.Web.ViewModels.Product;
    using TennisShopSystem.Web.ViewModels.Product.Enums;
    using TennisShopSystem.Web.ViewModels.Seller;

    //using Product = TennisShopSystem.Data.Models.Product;

    public class ProductService : IProductService
    {
        private readonly TennisShopDbContext dbContext;

        public ProductService(TennisShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AllProductsFilteredAndPagedServiceModel> AllAsync(AllProductQueryModel queryModel)
        {
            IQueryable<Product> productsQuery = this.dbContext
                .Products
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                productsQuery = productsQuery
                    .Where(p => p.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Brand))
            {
                productsQuery = productsQuery
                    .Where(p => p.Brand.Name == queryModel.Brand);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                productsQuery = productsQuery
                    .Where(p => EF.Functions.Like(p.Title, wildCard) ||
                                EF.Functions.Like(p.Description, wildCard));
            }

            productsQuery = queryModel.ProductSorting switch
            {
                ProductSorting.Newest => productsQuery.OrderByDescending(p => p.CreatedOn),
                ProductSorting.Oldest => productsQuery.OrderBy(p => p.CreatedOn),
                ProductSorting.PriceAscending => productsQuery.OrderBy(p => p.Price),
                ProductSorting.PriceDescending => productsQuery.OrderByDescending(p => p.Price),
                _ => productsQuery.OrderByDescending(p => p.CreatedOn)
            };

            IEnumerable<ProductAllViewModel> allProducts = await productsQuery
                .Where(p => p.IsAvailable)
                .Skip((queryModel.CurrentPage - 1) * queryModel.ProductsPerPage)
                .Take(queryModel.ProductsPerPage)
                .Select(p => new ProductAllViewModel
                {
                    Id = p.Id.ToString(),
                    Title = p.Title,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price
                })
                .ToArrayAsync();

            int totalProducts = productsQuery.Count();

            return new AllProductsFilteredAndPagedServiceModel()
            {
                TotalProductsCount = totalProducts,
                Products = allProducts
            };
        }

        public async Task<IEnumerable<ProductAllViewModel>> AllBySellerIdAsync(string sellerId)
        {
            IEnumerable<ProductAllViewModel> allSellerProducts = await this.dbContext
                .Products
                .Where(p => p.IsAvailable &&
                            p.SellerId.ToString() == sellerId)
                .Select(p => new ProductAllViewModel
                {
                    Id = p.Id.ToString(),
                    Title = p.Title,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price
                })
                .ToArrayAsync();

            return allSellerProducts;
        }

        public async Task<IEnumerable<ProductAllViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<ProductAllViewModel> allUserProducts = await this.dbContext
                .Products
                .Where(p => p.IsAvailable &&
                            p.BuyerId.HasValue &&
                            p.BuyerId.ToString() == userId)
                .Select(p => new ProductAllViewModel
                {
                    Id = p.Id.ToString(),
                    Title = p.Title,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price
                })
                .ToArrayAsync();

            return allUserProducts;
        }

        public async Task<string> CreateAndReturnIdAsync(ProductFormModel formModel, string sellerId)
        {
            Product newProduct = new Product()
            {
                Title = formModel.Title,
                Description = formModel.Description,
                ImageUrl = formModel.ImageUrl,
                Price = formModel.Price,
                Quantity = formModel.Quantity,
                CategoryId = formModel.CategoryId,
                BrandId = formModel.BrandId,
                SellerId = Guid.Parse(sellerId)
            };

            await this.dbContext.Products.AddAsync(newProduct);
            await this.dbContext.SaveChangesAsync();

            return newProduct.Id.ToString();
        }

        public async Task DeleteProductByIdAsync(string productId)
        {
            Product productToDelete = await this.dbContext
                .Products
                .Where(p => p.IsAvailable)
                .FirstAsync(p => p.Id.ToString() == productId);

            productToDelete.IsAvailable = false;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditProductByIdAndFormModelAsync(string productId, ProductFormModel formModel)
        {
            Product product = await this.dbContext
                .Products
                .Where(p => p.IsAvailable)
                .FirstAsync(p => p.Id.ToString() == productId);

            product.Title = formModel.Title;
            product.Description = formModel.Description;
            product.ImageUrl = formModel.ImageUrl;
            product.Price = formModel.Price;
            product.Quantity = formModel.Quantity;
            product.CategoryId = formModel.CategoryId;
            product.BrandId = formModel.BrandId;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string productId)
        {
            bool result = await this.dbContext
                .Products
                .Where(p => p.IsAvailable)
                .AnyAsync(p => p.Id.ToString() == productId);

            return result;
        }

        public async Task<ProductDetailsViewModel> GetDetailsByIdAsync(string productId)
        {
            Product product = await this.dbContext
                .Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Seller)
                .ThenInclude(s => s.User)
                .Where(p => p.IsAvailable)
                .FirstAsync(p => p.Id.ToString() == productId);

            return new ProductDetailsViewModel()
            {
                Id = product.Id.ToString(),
                Title = product.Title,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Brand = product.Brand.Name,
                Category = product.Category.Name,
                Seller = new SellerInfoOnProductViewModel()
                {
                    Email = product.Seller.User.Email,
                    PhoneNumber = product.Seller.PhoneNumber
                }
            };
        }

        public async Task<ProductPreDeleteDetailsViewModel> GetProductForDeleteByIdAsync(string productId)
        {
            Product product = await this.dbContext
                .Products
                .Where(p => p.IsAvailable)
                .FirstAsync(p => p.Id.ToString() == productId);

            return new ProductPreDeleteDetailsViewModel()
            {
                Title = product.Title,
                ImageUrl = product.ImageUrl
            };
        }

        public async Task<ProductFormModel> GetProductForEditByIdAsync(string productId)
        {
            Product product = await this.dbContext
                .Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Where(p => p.IsAvailable)
                .FirstAsync(p => p.Id.ToString() == productId);

            return new ProductFormModel()
            {
                Title = product.Title,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId
            };
        }

        public async Task<bool> IsSellerWithIdOwnerOfProductWithIdAsync(string productId, string sellerId)
        {
            Product product = await this.dbContext
                .Products
                .Where(p => p.IsAvailable)
                .FirstAsync(p => p.Id.ToString() == productId);

            return product.SellerId.ToString() == sellerId;
        }

        public async Task<IEnumerable<IndexViewModel>> LastThreeProductsAsync()
        {
            IEnumerable<IndexViewModel> lastThreeProducts = await dbContext
                .Products
                .Where(p => p.IsAvailable)
                .OrderByDescending(p => p.CreatedOn)
                .Take(3)
                .Select(p => new IndexViewModel()
                {
                    Id = p.Id.ToString(),
                    Title = p.Title,
                    ImageUrl = p.ImageUrl
                })
                .ToArrayAsync();

            return lastThreeProducts;
        }
    }
}
