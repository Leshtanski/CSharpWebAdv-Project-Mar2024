using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisShopSystem.Data.Migrations
{
    public partial class RemoveFromSeedIsAvailableSetToFalse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("277d2606-3bbe-4ec8-b545-0154d50aac89"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("87cef4c9-1fb2-4af6-98d7-4018817727e8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f31cd59e-34cb-4ef7-895d-899689fe7fd9"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "Description", "ImageUrl", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("9d8e6598-31b1-4ee0-8e7d-b820a7c2c67f"), 0, 2, null, 4, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://stringersworld-1f835.kxcdn.com/wp-content/uploads/2023/11/04kng3icvnr.png", null, 80.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Head Tennis Bag" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "Description", "ImageUrl", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("c7455fbc-19b9-4b11-992f-a86eebf0e7e6"), 0, 1, new Guid("cdf7d102-fa0d-4250-5bd1-08dc3cea7bb5"), 1, "This tennis racket was made with some experimental materials.", "https://pngset.com/images/nadal-babolat-tennis-racket-transparent-png-599557.png", null, 100.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Babolat Tennis Racket" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "Description", "ImageUrl", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("f103b59b-6b78-412b-b367-d2ae93ef78da"), 0, 9, null, 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://bityl.co/Obcm", null, 120.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Nike Tennis Shoe" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9d8e6598-31b1-4ee0-8e7d-b820a7c2c67f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c7455fbc-19b9-4b11-992f-a86eebf0e7e6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f103b59b-6b78-412b-b367-d2ae93ef78da"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsAvailable", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("277d2606-3bbe-4ec8-b545-0154d50aac89"), 0, 2, null, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://stringersworld-1f835.kxcdn.com/wp-content/uploads/2023/11/04kng3icvnr.png", false, null, 80.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Head Tennis Bag" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsAvailable", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("87cef4c9-1fb2-4af6-98d7-4018817727e8"), 0, 9, null, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://bityl.co/Obcm", false, null, 120.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Nike Tennis Shoe" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsAvailable", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("f31cd59e-34cb-4ef7-895d-899689fe7fd9"), 0, 1, new Guid("cdf7d102-fa0d-4250-5bd1-08dc3cea7bb5"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This tennis racket was made with some experimental materials.", "https://pngset.com/images/nadal-babolat-tennis-racket-transparent-png-599557.png", false, null, 100.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Babolat Tennis Racket" });
        }
    }
}
