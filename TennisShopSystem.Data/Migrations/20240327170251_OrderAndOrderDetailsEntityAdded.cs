using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisShopSystem.Data.Migrations
{
    public partial class OrderAndOrderDetailsEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("26572f49-d812-4589-a984-33e560065b65"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("57c85b25-92b1-4863-9c46-0ee65806fcec"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a84663ed-d93b-48ca-8499-500584e7603c"));

            migrationBuilder.AddColumn<int>(
                name: "AvailableQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderDetailsId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrdersDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_OrdersDetails_OrderDetailsId",
                        column: x => x.OrderDetailsId,
                        principalTable: "OrdersDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "Description", "ImageUrl", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("2b522fa1-e7f5-46c1-8542-10e237c90c99"), 0, 9, null, 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://bityl.co/Obcm", null, 120.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Nike Tennis Shoe" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "Description", "ImageUrl", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("904f5086-ff5a-40bf-9157-045c43c1fb0c"), 0, 2, null, 4, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://stringersworld-1f835.kxcdn.com/wp-content/uploads/2023/11/04kng3icvnr.png", null, 80.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Head Tennis Bag" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "BuyerId", "CategoryId", "Description", "ImageUrl", "OrderDetailsId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("d6e529aa-e110-4aad-9165-842e8e618504"), 0, 1, new Guid("cdf7d102-fa0d-4250-5bd1-08dc3cea7bb5"), 1, "This tennis racket was made with some experimental materials.", "https://pngset.com/images/nadal-babolat-tennis-racket-transparent-png-599557.png", null, 100.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Babolat Tennis Racket" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderDetailsId",
                table: "Products",
                column: "OrderDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderDetailsId",
                table: "Orders",
                column: "OrderDetailsId",
                unique: true,
                filter: "[OrderDetailsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_OrdersDetails_OrderDetailsId",
                table: "Products",
                column: "OrderDetailsId",
                principalTable: "OrdersDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_OrdersDetails_OrderDetailsId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OrdersDetails");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderDetailsId",
                table: "Products");

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

            migrationBuilder.DropColumn(
                name: "AvailableQuantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderDetailsId",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsAvailable", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("26572f49-d812-4589-a984-33e560065b65"), 2, null, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://stringersworld-1f835.kxcdn.com/wp-content/uploads/2023/11/04kng3icvnr.png", false, 80.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Head Tennis Bag" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsAvailable", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("57c85b25-92b1-4863-9c46-0ee65806fcec"), 1, new Guid("cdf7d102-fa0d-4250-5bd1-08dc3cea7bb5"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This tennis racket was made with some experimental materials.", "https://pngset.com/images/nadal-babolat-tennis-racket-transparent-png-599557.png", false, 100.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Babolat Tennis Racket" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsAvailable", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("a84663ed-d93b-48ca-8499-500584e7603c"), 9, null, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://bityl.co/Obcm", false, 120.00m, new Guid("c57fad0b-9bcd-4eb8-997e-ba644f197659"), "Nike Tennis Shoe" });
        }
    }
}
