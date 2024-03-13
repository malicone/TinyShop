using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyShop.Migrations
{
    public partial class DeliveryPaymentOrderStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TheDeliveryTypeId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TheOrderStatusId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThePaymentTypeId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeliveryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TheDeliveryTypeId",
                table: "Orders",
                column: "TheDeliveryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TheOrderStatusId",
                table: "Orders",
                column: "TheOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ThePaymentTypeId",
                table: "Orders",
                column: "ThePaymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryTypes_TheDeliveryTypeId",
                table: "Orders",
                column: "TheDeliveryTypeId",
                principalTable: "DeliveryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_TheOrderStatusId",
                table: "Orders",
                column: "TheOrderStatusId",
                principalTable: "OrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentTypes_ThePaymentTypeId",
                table: "Orders",
                column: "ThePaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryTypes_TheDeliveryTypeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_TheOrderStatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentTypes_ThePaymentTypeId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "DeliveryTypes");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TheDeliveryTypeId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TheOrderStatusId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ThePaymentTypeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TheDeliveryTypeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TheOrderStatusId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ThePaymentTypeId",
                table: "Orders");
        }
    }
}
