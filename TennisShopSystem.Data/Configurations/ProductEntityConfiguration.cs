﻿namespace TennisShopSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .Property(p => p.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(p => p.IsAvailable)
                .HasDefaultValue(true);


            builder
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Brand)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Seller)
                .WithMany(s => s.OwnedProducts)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property("Price")
                .HasPrecision(18, 2);

            builder.HasData(this.GenerateProducts());
        }

        private Product[] GenerateProducts()
        {
            ICollection<Product> products = new HashSet<Product>();

            Product product;

            product = new Product
            {
                Title = "Babolat Tennis Racket",
                Description = "This tennis racket was made with some experimental materials.",
                ImageUrl = "https://pngset.com/images/nadal-babolat-tennis-racket-transparent-png-599557.png",
                Price = 100.00M,
                CategoryId = 1,
                BrandId = 1,
            };
            products.Add(product);

            product = new Product
            {
                Title = "Nike Tennis Shoe",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.",
                ImageUrl = "https://bityl.co/Obcm",
                Price = 120.00M,
                CategoryId = 3,
                BrandId = 9,
            };
            products.Add(product);

            product = new Product
            {
                Title = "Head Tennis Bag",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.",
                ImageUrl = "https://stringersworld-1f835.kxcdn.com/wp-content/uploads/2023/11/04kng3icvnr.png",
                Price = 80.00M,
                CategoryId = 4,
                BrandId = 2,
            };
            products.Add(product);

            return products.ToArray();
        }
    }
}
