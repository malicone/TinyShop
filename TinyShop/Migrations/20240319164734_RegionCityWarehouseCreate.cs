using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyShop.Migrations
{
    public partial class RegionCityWarehouseCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    TheDeliveryTypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    IdExternal = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    RawJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_DeliveryTypes_TheDeliveryTypeId",
                        column: x => x.TheDeliveryTypeId,
                        principalTable: "DeliveryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    TheDeliveryTypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    IdExternal = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    RawJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseTypes_DeliveryTypes_TheDeliveryTypeId",
                        column: x => x.TheDeliveryTypeId,
                        principalTable: "DeliveryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    TypeDescription = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Index = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    RegionIdExternal = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    TheRegionId = table.Column<int>(type: "int", nullable: true),
                    TheDeliveryTypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    IdExternal = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    RawJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_DeliveryTypes_TheDeliveryTypeId",
                        column: x => x.TheDeliveryTypeId,
                        principalTable: "DeliveryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cities_Regions_TheRegionId",
                        column: x => x.TheRegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CityIdExternal = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    WarehouseTypeIdExternal = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    TheCityId = table.Column<int>(type: "int", nullable: true),
                    TheDeliveryTypeId = table.Column<int>(type: "int", nullable: true),
                    TheWarehouseTypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    IdExternal = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    RawJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Cities_TheCityId",
                        column: x => x.TheCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warehouses_DeliveryTypes_TheDeliveryTypeId",
                        column: x => x.TheDeliveryTypeId,
                        principalTable: "DeliveryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warehouses_WarehouseTypes_TheWarehouseTypeId",
                        column: x => x.TheWarehouseTypeId,
                        principalTable: "WarehouseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_IdExternal",
                table: "Cities",
                column: "IdExternal");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_RegionIdExternal",
                table: "Cities",
                column: "RegionIdExternal");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_TheDeliveryTypeId",
                table: "Cities",
                column: "TheDeliveryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_TheRegionId",
                table: "Cities",
                column: "TheRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_IdExternal",
                table: "Regions",
                column: "IdExternal");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_TheDeliveryTypeId",
                table: "Regions",
                column: "TheDeliveryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_CityIdExternal",
                table: "Warehouses",
                column: "CityIdExternal");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_IdExternal",
                table: "Warehouses",
                column: "IdExternal");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_TheCityId",
                table: "Warehouses",
                column: "TheCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_TheDeliveryTypeId",
                table: "Warehouses",
                column: "TheDeliveryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_TheWarehouseTypeId",
                table: "Warehouses",
                column: "TheWarehouseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_WarehouseTypeIdExternal",
                table: "Warehouses",
                column: "WarehouseTypeIdExternal");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseTypes_IdExternal",
                table: "WarehouseTypes",
                column: "IdExternal");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseTypes_TheDeliveryTypeId",
                table: "WarehouseTypes",
                column: "TheDeliveryTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "WarehouseTypes");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
