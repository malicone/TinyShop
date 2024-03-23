using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyShop.Migrations
{
    public partial class DeliveryTypeSoftDeletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DeliveryTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DeliveryTypes",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SoftDeletedAt",
                table: "DeliveryTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoftDeletedBy",
                table: "DeliveryTypes",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "DeliveryTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "DeliveryTypes",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DeliveryTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DeliveryTypes");

            migrationBuilder.DropColumn(
                name: "SoftDeletedAt",
                table: "DeliveryTypes");

            migrationBuilder.DropColumn(
                name: "SoftDeletedBy",
                table: "DeliveryTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "DeliveryTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DeliveryTypes");
        }
    }
}
