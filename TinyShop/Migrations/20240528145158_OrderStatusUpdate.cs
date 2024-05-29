using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyShop.Migrations
{
    public partial class OrderStatusUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryTypes_DeliveryFirms_TheDeliveryFirmId",
                table: "DeliveryTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_TheCustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryTypes_TheDeliveryTypeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentTypes_ThePaymentTypeId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OrderStatuses",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "OrderStatuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "OrderStatuses",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SoftDeletedAt",
                table: "OrderStatuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoftDeletedBy",
                table: "OrderStatuses",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SortingColumn",
                table: "OrderStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "OrderStatuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "OrderStatuses",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ThePaymentTypeId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TheDeliveryTypeId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TheCustomerId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TheDeliveryFirmId",
                table: "DeliveryTypes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryTypes_DeliveryFirms_TheDeliveryFirmId",
                table: "DeliveryTypes",
                column: "TheDeliveryFirmId",
                principalTable: "DeliveryFirms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_TheCustomerId",
                table: "Orders",
                column: "TheCustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryTypes_TheDeliveryTypeId",
                table: "Orders",
                column: "TheDeliveryTypeId",
                principalTable: "DeliveryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentTypes_ThePaymentTypeId",
                table: "Orders",
                column: "ThePaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryTypes_DeliveryFirms_TheDeliveryFirmId",
                table: "DeliveryTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_TheCustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryTypes_TheDeliveryTypeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentTypes_ThePaymentTypeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "OrderStatuses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OrderStatuses");

            migrationBuilder.DropColumn(
                name: "SoftDeletedAt",
                table: "OrderStatuses");

            migrationBuilder.DropColumn(
                name: "SoftDeletedBy",
                table: "OrderStatuses");

            migrationBuilder.DropColumn(
                name: "SortingColumn",
                table: "OrderStatuses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "OrderStatuses");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "OrderStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OrderStatuses",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<int>(
                name: "ThePaymentTypeId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TheDeliveryTypeId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TheCustomerId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TheDeliveryFirmId",
                table: "DeliveryTypes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryTypes_DeliveryFirms_TheDeliveryFirmId",
                table: "DeliveryTypes",
                column: "TheDeliveryFirmId",
                principalTable: "DeliveryFirms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_TheCustomerId",
                table: "Orders",
                column: "TheCustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryTypes_TheDeliveryTypeId",
                table: "Orders",
                column: "TheDeliveryTypeId",
                principalTable: "DeliveryTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentTypes_ThePaymentTypeId",
                table: "Orders",
                column: "ThePaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "Id");
        }
    }
}
