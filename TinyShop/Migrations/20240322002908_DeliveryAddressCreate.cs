using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyShop.Migrations
{
    public partial class DeliveryAddressCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TheDeliveryAddressId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeliveryAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TheRegionId = table.Column<int>(type: "int", nullable: true),
                    RegionNameSnapshot = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    TheCityId = table.Column<int>(type: "int", nullable: true),
                    CityNameSnapshot = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    TheWarehouseId = table.Column<int>(type: "int", nullable: true),
                    WarehouseNameSnapshot = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryAddresses_Cities_TheCityId",
                        column: x => x.TheCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryAddresses_Regions_TheRegionId",
                        column: x => x.TheRegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryAddresses_Warehouses_TheWarehouseId",
                        column: x => x.TheWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TheDeliveryAddressId",
                table: "Orders",
                column: "TheDeliveryAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddresses_TheCityId",
                table: "DeliveryAddresses",
                column: "TheCityId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddresses_TheRegionId",
                table: "DeliveryAddresses",
                column: "TheRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddresses_TheWarehouseId",
                table: "DeliveryAddresses",
                column: "TheWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryAddresses_TheDeliveryAddressId",
                table: "Orders",
                column: "TheDeliveryAddressId",
                principalTable: "DeliveryAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryAddresses_TheDeliveryAddressId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "DeliveryAddresses");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TheDeliveryAddressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TheDeliveryAddressId",
                table: "Orders");
        }
    }
}
