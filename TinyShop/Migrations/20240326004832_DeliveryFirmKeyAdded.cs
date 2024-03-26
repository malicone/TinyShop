using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyShop.Migrations
{
    public partial class DeliveryFirmKeyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TheDeliveryFirmId",
                table: "DeliveryTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryTypes_TheDeliveryFirmId",
                table: "DeliveryTypes",
                column: "TheDeliveryFirmId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryTypes_DeliveryFirms_TheDeliveryFirmId",
                table: "DeliveryTypes",
                column: "TheDeliveryFirmId",
                principalTable: "DeliveryFirms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryTypes_DeliveryFirms_TheDeliveryFirmId",
                table: "DeliveryTypes");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryTypes_TheDeliveryFirmId",
                table: "DeliveryTypes");

            migrationBuilder.DropColumn(
                name: "TheDeliveryFirmId",
                table: "DeliveryTypes");
        }
    }
}
