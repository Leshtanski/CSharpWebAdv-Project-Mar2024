using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisShopSystem.Data.Migrations
{
    public partial class FixDateTimeValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a9f4163c-25f0-4355-ac28-71e9152ba352"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("eccf6828-cc19-40e3-80a0-c9ce748290c9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fd841dcd-17d1-453e-b3d4-abb5f39789cb"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 7, 13, 42, 4, 429, DateTimeKind.Utc).AddTicks(9743));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "BuyerId", "CategoryId", "Description", "ImageUrl", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("12419058-dc50-4d81-ad93-3a0a8c93346e"), 2, null, 4, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://stringersworld-1f835.kxcdn.com/wp-content/uploads/2023/11/04kng3icvnr.png", 80.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Head Tennis Bag" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "BuyerId", "CategoryId", "Description", "ImageUrl", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("5faf776a-69d3-4840-97ad-e49a04525040"), 1, new Guid("cdf7d102-fa0d-4250-5bd1-08dc3cea7bb5"), 1, "This tennis racket was made with some experimental materials.", "https://pngset.com/images/nadal-babolat-tennis-racket-transparent-png-599557.png", 100.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Babolat Tennis Racket" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "BuyerId", "CategoryId", "Description", "ImageUrl", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("6e69f7ef-d471-46a8-9304-9e15be2b05db"), 9, null, 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://bityl.co/Obcm", 120.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Nike Tennis Shoe" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("12419058-dc50-4d81-ad93-3a0a8c93346e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5faf776a-69d3-4840-97ad-e49a04525040"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6e69f7ef-d471-46a8-9304-9e15be2b05db"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 7, 13, 42, 4, 429, DateTimeKind.Utc).AddTicks(9743),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("a9f4163c-25f0-4355-ac28-71e9152ba352"), 2, null, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://stringersworld-1f835.kxcdn.com/wp-content/uploads/2023/11/04kng3icvnr.png", 80.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Head Tennis Bag" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("eccf6828-cc19-40e3-80a0-c9ce748290c9"), 1, new Guid("cdf7d102-fa0d-4250-5bd1-08dc3cea7bb5"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This tennis racket was made with some experimental materials.", "https://pngset.com/images/nadal-babolat-tennis-racket-transparent-png-599557.png", 100.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Babolat Tennis Racket" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("fd841dcd-17d1-453e-b3d4-abb5f39789cb"), 9, null, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://bityl.co/Obcm", 120.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Nike Tennis Shoe" });
        }
    }
}
