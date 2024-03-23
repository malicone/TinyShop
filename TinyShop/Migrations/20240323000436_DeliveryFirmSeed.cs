using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyShop.Migrations
{
    public partial class DeliveryFirmSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DeliveryFirms",
                columns: new[] { "Id", "Name", "SortingColumn" },
                values: new object[] { 1, "Нова Пошта", 10 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql( "DELETE FROM DeliveryFirms" );
            migrationBuilder.Sql( "DBCC CHECKIDENT ('[DeliveryFirms]', RESEED, 0)" );
        }
    }
}
