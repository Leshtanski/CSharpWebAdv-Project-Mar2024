﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisShopSystem.Data.Migrations
{
    public partial class DropColumnCreatedOnAndAddColumnOrderedOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6ea4aff1-2360-4d58-a468-7c2fe298ff8e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c0cc90c7-ee7d-421b-af0f-39714ba66172"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c3be2047-4c70-45f6-96b5-2138e98f9bc5"));

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "OrdersDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderedOn",
                table: "OrdersDetails",
                type: "DateTime",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "CategoryId", "Description", "ImageUrl", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("3bf94754-a22c-420e-b679-a858006772cb"), 10, 9, 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://bityl.co/Obcm", 120.00m, null, "Nike Tennis Shoe" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "CategoryId", "Description", "ImageUrl", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("7d45b8ed-cf05-47a5-9fc1-ce2ea038c668"), 10, 2, 4, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://stringersworld-1f835.kxcdn.com/wp-content/uploads/2023/11/04kng3icvnr.png", 80.00m, null, "Head Tennis Bag" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "CategoryId", "Description", "ImageUrl", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("982cf933-b5f5-4f5c-b953-a578d7297bb6"), 10, 1, 1, "This tennis racket was made with some experimental materials.", "https://pngset.com/images/nadal-babolat-tennis-racket-transparent-png-599557.png", 100.00m, null, "Babolat Tennis Racket" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3bf94754-a22c-420e-b679-a858006772cb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7d45b8ed-cf05-47a5-9fc1-ce2ea038c668"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("982cf933-b5f5-4f5c-b953-a578d7297bb6"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsAvailable", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("6ea4aff1-2360-4d58-a468-7c2fe298ff8e"), 10, 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This tennis racket was made with some experimental materials.", "https://pngset.com/images/nadal-babolat-tennis-racket-transparent-png-599557.png", false, 100.00m, null, "Babolat Tennis Racket" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsAvailable", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("c0cc90c7-ee7d-421b-af0f-39714ba66172"), 10, 9, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://bityl.co/Obcm", false, 120.00m, null, "Nike Tennis Shoe" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "BrandId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsAvailable", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("c3be2047-4c70-45f6-96b5-2138e98f9bc5"), 10, 2, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent feugiat tempus lorem et porttitor. Donec aliquam laoreet sem sit amet malesuada.", "https://stringersworld-1f835.kxcdn.com/wp-content/uploads/2023/11/04kng3icvnr.png", false, 80.00m, null, "Head Tennis Bag" });
        }
    }
}
