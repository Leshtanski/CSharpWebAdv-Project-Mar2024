using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisShopSystem.Data.Models;

namespace TennisShopSystem.Data.Configurations
{
    public class OrderedItemEntityConfiguration : IEntityTypeConfiguration<OrderedItem>
    {
        public void Configure(EntityTypeBuilder<OrderedItem> builder)
        {
            builder
                .HasOne(oi => oi.OrderDetails)
                .WithMany(od => od.OrderedItems)
                .HasForeignKey(od => od.OrderDetailsId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
