namespace TennisShopSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public class BrandEntityConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasData(this.GenerateBrands());
        }

        private Brand[] GenerateBrands()
        {
            ICollection<Brand> brands = new HashSet<Brand>();

            Brand brand;

            brand = new Brand()
            {
                Id = 1,
                Name = "Babolat"
            };
            brands.Add(brand);

            brand = new Brand()
            {
                Id = 2,
                Name = "Head"
            };
            brands.Add(brand);

            brand = new Brand()
            {
                Id = 3,
                Name = "Technifibre"
            };
            brands.Add(brand);

            brand = new Brand()
            {
                Id = 4,
                Name = "Wilson"
            };
            brands.Add(brand);

            brand = new Brand()
            {
                Id = 5,
                Name = "Yonex"
            };
            brands.Add(brand);

            brand = new Brand()
            {
                Id = 6,
                Name = "Adidas"
            };
            brands.Add(brand);

            brand = new Brand()
            {
                Id = 7,
                Name = "Asics"
            };
            brands.Add(brand);

            brand = new Brand()
            {
                Id = 8,
                Name = "Lacoste"
            };
            brands.Add(brand);

            brand = new Brand()
            {
                Id = 9,
                Name = "Nike"
            };
            brands.Add(brand);

            brand = new Brand()
            {
                Id = 10,
                Name = "Under Armour"
            };
            brands.Add(brand);

            return brands.ToArray();
        }
    }
}
