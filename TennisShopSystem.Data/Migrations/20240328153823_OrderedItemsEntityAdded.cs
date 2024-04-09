#nullable disable

namespace TennisShopSystem.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class OrderedItemsEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderedItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderedQuantity = table.Column<int>(type: "int", nullable: false),
                    OrderDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderedItems_OrdersDetails_OrderDetailsId",
                        column: x => x.OrderDetailsId,
                        principalTable: "OrdersDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "Description", "ImageUrl", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("37fd4334-5f4a-48f9-99cf-fd571fe4445b"), 0, 1, new Guid("cdf7d102-fa0d-4250-5bd1-08dc3cea7bb5"), 1, "This tennis racket was made with some experimental materials.", "https://pngset.com/images/nadal-babolat-tennis-racket-transparent-png-599557.png", null, 100.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Babolat Tennis Racket" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "Description", "ImageUrl", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("9d4c4526-cf38-46d8-833c-48b6bcbec83c"), 0, 2, null, 4, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://stringersworld-1f835.kxcdn.com/wp-content/uploads/2023/11/04kng3icvnr.png", null, 80.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Head Tennis Bag" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "Description", "ImageUrl", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("c8fd2b7a-3c30-4473-a762-650212242c42"), 0, 9, null, 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://bityl.co/Obcm", null, 120.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Nike Tennis Shoe" });


            migrationBuilder.CreateIndex(
                name: "IX_OrderedItems_OrderDetailsId",
                table: "OrderedItems",
                column: "OrderDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderedItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "OrdersDetails");

            migrationBuilder.DropTable(
                name: "Sellers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
