namespace TennisShopSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TennisShopSystem.Data;
    using TennisShopSystem.Data.Models;
    using TennisShopSystem.Services.Data.Interfaces;
    using TennisShopSystem.Web.ViewModels.Home;
    using TennisShopSystem.Web.ViewModels.Product;
    public class ProductService : IProductService
    {
        private readonly TennisShopDbContext dbContext;

        public ProductService(TennisShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(ProductFormModel formModel, string sellerId)
        {
            Product newProduct = new Product()
            {
                Title = formModel.Title,
                Description = formModel.Description,
                ImageUrl = formModel.ImageUrl,
                Price = formModel.Price,
                CategoryId = formModel.CategoryId,
                BrandId = formModel.BrandId,
                SellerId = Guid.Parse(sellerId)
            };

            await this.dbContext.Products.AddAsync(newProduct);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<IndexViewModel>> LastThreeProductsAsync()
        {
            IEnumerable<IndexViewModel> lastThreeProducts = await dbContext
                .Products
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
