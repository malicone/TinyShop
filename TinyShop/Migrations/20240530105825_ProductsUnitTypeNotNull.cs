using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyShop.Migrations
{
    public partial class ProductsUnitTypeNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductUnitTypes_UnitTypeId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "UnitTypeId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductUnitTypes_UnitTypeId",
                table: "Products",
                column: "UnitTypeId",
                principalTable: "ProductUnitTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductUnitTypes_UnitTypeId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "UnitTypeId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductUnitTypes_UnitTypeId",
                table: "Products",
                column: "UnitTypeId",
                principalTable: "ProductUnitTypes",
                principalColumn: "Id");
        }
    }
}
