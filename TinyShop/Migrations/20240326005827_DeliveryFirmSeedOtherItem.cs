using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyShop.Migrations
{
    public partial class DeliveryFirmSeedOtherItem : Migration
    {
        protected override void Up( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.Sql( "INSERT INTO DeliveryFirms (Name, SortingColumn) VALUES (N'Інше', 1000)" );
            migrationBuilder.Sql( "UPDATE DeliveryTypes SET SortingColumn = 10 WHERE Name = N'Самовивіз (м. Луцьк)'" );
            migrationBuilder.Sql( "UPDATE DeliveryTypes SET SortingColumn = 100 WHERE Name = N'Нова Пошта (на відділення)'" );

            migrationBuilder.Sql( "UPDATE DeliveryTypes SET TheDeliveryFirmId = 1 WHERE Name = N'Нова Пошта (на відділення)'" );
            migrationBuilder.Sql( "UPDATE DeliveryTypes SET TheDeliveryFirmId = 2 WHERE Name = N'Самовивіз (м. Луцьк)'" );
        }

        protected override void Down( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.Sql( "UPDATE DeliveryTypes SET DeliveryFirmId = NULL WHERE Name = N'Нова Пошта (на відділення)'" );
            migrationBuilder.Sql( "UPDATE DeliveryTypes SET DeliveryFirmId = NULL WHERE Name = N'Самовивіз (м. Луцьк)'" );            
            migrationBuilder.Sql( "DELETE FROM DeliveryFirms WHERE Name = N'Інше'" );
        }
    }
}