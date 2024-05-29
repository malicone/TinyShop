using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyShop.Migrations
{
    public partial class AddUnitTypeFieldToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitTypeId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitTypeId",
                table: "Products",
                column: "UnitTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductUnitTypes_UnitTypeId",
                table: "Products",
                column: "UnitTypeId",
                principalTable: "ProductUnitTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductUnitTypes_UnitTypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UnitTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UnitTypeId",
                table: "Products");
        }
    }
}
