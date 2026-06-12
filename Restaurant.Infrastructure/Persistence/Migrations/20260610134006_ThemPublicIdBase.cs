using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ThemPublicIdBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PublicId",
                table: "RestaurantTables",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "newid()");

            migrationBuilder.AddColumn<Guid>(
                name: "PublicId",
                table: "ProductStocks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "newid()");

            migrationBuilder.AddColumn<Guid>(
                name: "PublicId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "newid()");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_PublicId",
                table: "RestaurantTables",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_PublicId",
                table: "ProductStocks",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_PublicId",
                table: "Categories",
                column: "PublicId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tables_PublicId",
                table: "RestaurantTables");

            migrationBuilder.DropIndex(
                name: "IX_ProductStocks_PublicId",
                table: "ProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_Categories_PublicId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "RestaurantTables");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "ProductStocks");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Categories");
        }
    }
}
