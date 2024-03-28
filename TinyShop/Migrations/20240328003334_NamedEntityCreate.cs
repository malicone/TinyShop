using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyShop.Migrations
{
    public partial class NamedEntityCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PaymentTypes",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PaymentTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PaymentTypes",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SoftDeletedAt",
                table: "PaymentTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoftDeletedBy",
                table: "PaymentTypes",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SortingColumn",
                table: "PaymentTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PaymentTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "PaymentTypes",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DeliveryTypes",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "SoftDeletedAt",
                table: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "SoftDeletedBy",
                table: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "SortingColumn",
                table: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PaymentTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PaymentTypes",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DeliveryTypes",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);
        }
    }
}
