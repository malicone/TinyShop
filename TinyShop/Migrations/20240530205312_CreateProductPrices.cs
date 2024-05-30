using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyShop.Migrations
{
    public partial class CreateProductPrices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WholesalePriceNegotiable",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MinPackSaleQuantitySnapshot",
                table: "OrderLines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerUnitSnapshot",
                table: "OrderLines",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UnitsPerPackSnapshot",
                table: "OrderLines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TheProductId = table.Column<int>(type: "int", nullable: false),
                    PricePerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitsPerPack = table.Column<int>(type: "int", nullable: false),
                    MinPackSaleQuantity = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPrices_Products_TheProductId",
                        column: x => x.TheProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_TheProductId",
                table: "ProductPrices",
                column: "TheProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPrices");

            migrationBuilder.DropColumn(
                name: "WholesalePriceNegotiable",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MinPackSaleQuantitySnapshot",
                table: "OrderLines");

            migrationBuilder.DropColumn(
                name: "PricePerUnitSnapshot",
                table: "OrderLines");

            migrationBuilder.DropColumn(
                name: "UnitsPerPackSnapshot",
                table: "OrderLines");
        }
    }
}
