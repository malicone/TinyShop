using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyShop.Migrations
{
    public partial class DeliveryPaymentOrderStatusSeed : Migration
    {
        protected override void Up( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.Sql( "INSERT INTO DeliveryTypes(Name) VALUES(N'Самовивіз (м. Луцьк)')" );            
            migrationBuilder.Sql( "INSERT INTO DeliveryTypes(Name) VALUES(N'Нова Пошта (на відділення)')" );

            migrationBuilder.Sql( "INSERT INTO OrderStatuses(Name) VALUES(N'Новий')" );
            migrationBuilder.Sql( "INSERT INTO OrderStatuses(Name) VALUES(N'Обробляється')" );
            migrationBuilder.Sql( "INSERT INTO OrderStatuses(Name) VALUES(N'Завершений')" );
            migrationBuilder.Sql( "INSERT INTO OrderStatuses(Name) VALUES(N'Відмінений')" );

            migrationBuilder.Sql( "INSERT INTO PaymentTypes(Name) VALUES(N'На картку')" );
            migrationBuilder.Sql( "INSERT INTO PaymentTypes(Name) VALUES(N'Накладений платіж')" );            
        }

        protected override void Down( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.Sql( "DELETE FROM DeliveryTypes" );
            migrationBuilder.Sql( "DBCC CHECKIDENT ('[DeliveryTypes]', RESEED, 0)" );

            migrationBuilder.Sql( "DELETE FROM OrderStatuses" );
            migrationBuilder.Sql( "DBCC CHECKIDENT ('[OrderStatuses]', RESEED, 0)" );

            migrationBuilder.Sql( "DELETE FROM PaymentTypes" );
            migrationBuilder.Sql( "DBCC CHECKIDENT ('[PaymentTypes]', RESEED, 0)" );
        }
    }
}