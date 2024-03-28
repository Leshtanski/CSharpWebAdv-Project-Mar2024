namespace TennisShopSystem.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    using Models;
    
    public class TennisShopDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public TennisShopDbContext(DbContextOptions<TennisShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Brand> Brands { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Seller> Sellers { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<OrderDetails> OrdersDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(TennisShopDbContext)) ??
                                      Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(configAssembly);

            base.OnModelCreating(builder);
        }
    }
}
