using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyShop.Migrations
{
    public partial class SeedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql( @"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], 
                    [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], 
                    [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) 
                VALUES (N'06afe865-1f49-4599-98a7-ab154a61d93e', N'tinyshop@gmail.com', N'TINYSHOP@GMAIL.COM', N'tinyshop@gmail.com', 
                    N'TINYSHOP@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEIZ4Y45GJ2zg5KyNORodM4RTrkJMgYVxZ45tlDtUEkbgGlw5WLT6dIgOy92m4FqNhQ==', 
                    N'ROMT73KV4BNTRIT6YLX2LODGWZG6QC6G', N'58a91e2e-97dd-4cb5-9fcc-b9afd23be411', NULL, 0, 0, NULL, 1, 0)
            " );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
