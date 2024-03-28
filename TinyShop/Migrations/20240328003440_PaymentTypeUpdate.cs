using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyShop.Migrations
{
    public partial class PaymentTypeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql( @"UPDATE PaymentTypes SET 
Name = N'Післясплата (наложений платіж)',
SortingColumn = 200
WHERE Name = N'Накладений платіж'" );
            migrationBuilder.Sql( @"UPDATE PaymentTypes SET 
SortingColumn = 100
WHERE Name = N'На картку'" );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql( @"UPDATE PaymentTypes SET 
Name = N'Накладений платіж',
SortingColumn = 0
WHERE Name = N'Післясплата (наложений платіж)'" );
            migrationBuilder.Sql( @"UPDATE PaymentTypes SET 
SortingColumn = 0
WHERE Name = N'На картку'" );
        }
    }
}
