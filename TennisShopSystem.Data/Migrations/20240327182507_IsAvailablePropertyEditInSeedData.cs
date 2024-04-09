#nullable disable

namespace TennisShopSystem.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class IsAvailablePropertyEditInSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2b522fa1-e7f5-46c1-8542-10e237c90c99"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("904f5086-ff5a-40bf-9157-045c43c1fb0c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d6e529aa-e110-4aad-9165-842e8e618504"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "Description", "ImageUrl", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("277d2606-3bbe-4ec8-b545-0154d50aac89"), 0, 2, null, 4, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://stringersworld-1f835.kxcdn.com/wp-content/uploads/2023/11/04kng3icvnr.png", null, 80.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Head Tennis Bag" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "Description", "ImageUrl", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("87cef4c9-1fb2-4af6-98d7-4018817727e8"), 0, 9, null, 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://bityl.co/Obcm", null, 120.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Nike Tennis Shoe" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "Description", "ImageUrl", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("f31cd59e-34cb-4ef7-895d-899689fe7fd9"), 0, 1, new Guid("cdf7d102-fa0d-4250-5bd1-08dc3cea7bb5"), 1, "This tennis racket was made with some experimental materials.", "https://pngset.com/images/nadal-babolat-tennis-racket-transparent-png-599557.png", null, 100.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Babolat Tennis Racket" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsAvailable", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("2b522fa1-e7f5-46c1-8542-10e237c90c99"), 0, 9, null, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://bityl.co/Obcm", false, null, 120.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Nike Tennis Shoe" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsAvailable", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("904f5086-ff5a-40bf-9157-045c43c1fb0c"), 0, 2, null, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://stringersworld-1f835.kxcdn.com/wp-content/uploads/2023/11/04kng3icvnr.png", false, null, 80.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Head Tennis Bag" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsAvailable", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("d6e529aa-e110-4aad-9165-842e8e618504"), 0, 1, new Guid("cdf7d102-fa0d-4250-5bd1-08dc3cea7bb5"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This tennis racket was made with some experimental materials.", "https://pngset.com/images/nadal-babolat-tennis-racket-transparent-png-599557.png", false, null, 100.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Babolat Tennis Racket" });
        }
    }
}
