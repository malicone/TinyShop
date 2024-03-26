using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyShop.Migrations
{
    public partial class DeliveryTypeToDeliveryFirm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql( "UPDATE Regions SET TheDeliveryTypeId = 1" );
            migrationBuilder.Sql( "UPDATE Cities SET TheDeliveryTypeId = 1" );
            migrationBuilder.Sql( "UPDATE Warehouses SET TheDeliveryTypeId = 1" );
            migrationBuilder.Sql( "UPDATE WarehouseTypes SET TheDeliveryTypeId = 1" );

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_DeliveryTypes_TheDeliveryTypeId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_DeliveryTypes_TheDeliveryTypeId",
                table: "Regions");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_DeliveryTypes_TheDeliveryTypeId",
                table: "Warehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseTypes_DeliveryTypes_TheDeliveryTypeId",
                table: "WarehouseTypes");

            migrationBuilder.RenameColumn(
                name: "TheDeliveryTypeId",
                table: "WarehouseTypes",
                newName: "TheDeliveryFirmId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseTypes_TheDeliveryTypeId",
                table: "WarehouseTypes",
                newName: "IX_WarehouseTypes_TheDeliveryFirmId");

            migrationBuilder.RenameColumn(
                name: "TheDeliveryTypeId",
                table: "Warehouses",
                newName: "TheDeliveryFirmId");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouses_TheDeliveryTypeId",
                table: "Warehouses",
                newName: "IX_Warehouses_TheDeliveryFirmId");

            migrationBuilder.RenameColumn(
                name: "TheDeliveryTypeId",
                table: "Regions",
                newName: "TheDeliveryFirmId");

            migrationBuilder.RenameIndex(
                name: "IX_Regions_TheDeliveryTypeId",
                table: "Regions",
                newName: "IX_Regions_TheDeliveryFirmId");

            migrationBuilder.RenameColumn(
                name: "TheDeliveryTypeId",
                table: "Cities",
                newName: "TheDeliveryFirmId");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_TheDeliveryTypeId",
                table: "Cities",
                newName: "IX_Cities_TheDeliveryFirmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_DeliveryFirms_TheDeliveryFirmId",
                table: "Cities",
                column: "TheDeliveryFirmId",
                principalTable: "DeliveryFirms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_DeliveryFirms_TheDeliveryFirmId",
                table: "Regions",
                column: "TheDeliveryFirmId",
                principalTable: "DeliveryFirms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_DeliveryFirms_TheDeliveryFirmId",
                table: "Warehouses",
                column: "TheDeliveryFirmId",
                principalTable: "DeliveryFirms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseTypes_DeliveryFirms_TheDeliveryFirmId",
                table: "WarehouseTypes",
                column: "TheDeliveryFirmId",
                principalTable: "DeliveryFirms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_DeliveryFirms_TheDeliveryFirmId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_DeliveryFirms_TheDeliveryFirmId",
                table: "Regions");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_DeliveryFirms_TheDeliveryFirmId",
                table: "Warehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseTypes_DeliveryFirms_TheDeliveryFirmId",
                table: "WarehouseTypes");

            migrationBuilder.RenameColumn(
                name: "TheDeliveryFirmId",
                table: "WarehouseTypes",
                newName: "TheDeliveryTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseTypes_TheDeliveryFirmId",
                table: "WarehouseTypes",
                newName: "IX_WarehouseTypes_TheDeliveryTypeId");

            migrationBuilder.RenameColumn(
                name: "TheDeliveryFirmId",
                table: "Warehouses",
                newName: "TheDeliveryTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouses_TheDeliveryFirmId",
                table: "Warehouses",
                newName: "IX_Warehouses_TheDeliveryTypeId");

            migrationBuilder.RenameColumn(
                name: "TheDeliveryFirmId",
                table: "Regions",
                newName: "TheDeliveryTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Regions_TheDeliveryFirmId",
                table: "Regions",
                newName: "IX_Regions_TheDeliveryTypeId");

            migrationBuilder.RenameColumn(
                name: "TheDeliveryFirmId",
                table: "Cities",
                newName: "TheDeliveryTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_TheDeliveryFirmId",
                table: "Cities",
                newName: "IX_Cities_TheDeliveryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_DeliveryTypes_TheDeliveryTypeId",
                table: "Cities",
                column: "TheDeliveryTypeId",
                principalTable: "DeliveryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_DeliveryTypes_TheDeliveryTypeId",
                table: "Regions",
                column: "TheDeliveryTypeId",
                principalTable: "DeliveryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_DeliveryTypes_TheDeliveryTypeId",
                table: "Warehouses",
                column: "TheDeliveryTypeId",
                principalTable: "DeliveryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseTypes_DeliveryTypes_TheDeliveryTypeId",
                table: "WarehouseTypes",
                column: "TheDeliveryTypeId",
                principalTable: "DeliveryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
