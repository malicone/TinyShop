using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyShop.Migrations
{
    public partial class DeliveryFirmFKSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql( "UPDATE Regions SET TheDeliveryFirmId = 1" );
            migrationBuilder.Sql( "UPDATE Cities SET TheDeliveryFirmId = 1" );
            migrationBuilder.Sql( "UPDATE Warehouses SET TheDeliveryFirmId = 1" );
            migrationBuilder.Sql( "UPDATE WarehouseTypes SET TheDeliveryFirmId = 1" );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql( "UPDATE Regions SET TheDeliveryFirmId = NULL" );
            migrationBuilder.Sql( "UPDATE Cities SET TheDeliveryFirmId = NULL" );
            migrationBuilder.Sql( "UPDATE Warehouses SET TheDeliveryFirmId = NULL" );
            migrationBuilder.Sql( "UPDATE WarehouseTypes SET TheDeliveryFirmId = NULL" );
        }
    }
}
