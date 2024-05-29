using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyShop.Migrations
{
    public partial class CreateProductProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SortingColumn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProperties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductUnitTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SortingColumn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUnitTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductProductProperty",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    PropertiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductProperty", x => new { x.ProductsId, x.PropertiesId });
                    table.ForeignKey(
                        name: "FK_ProductProductProperty_ProductProperties_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "ProductProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductProperty_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPropertyItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TheProductPropertyId = table.Column<int>(type: "int", nullable: false),
                    TheFileTagId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeletedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SortingColumn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPropertyItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPropertyItems_FileTags_TheFileTagId",
                        column: x => x.TheFileTagId,
                        principalTable: "FileTags",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductPropertyItems_ProductProperties_TheProductPropertyId",
                        column: x => x.TheProductPropertyId,
                        principalTable: "ProductProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderLineProductPropertyItem",
                columns: table => new
                {
                    OrderLinesId = table.Column<int>(type: "int", nullable: false),
                    PropertyItemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLineProductPropertyItem", x => new { x.OrderLinesId, x.PropertyItemsId });
                    table.ForeignKey(
                        name: "FK_OrderLineProductPropertyItem_OrderLines_OrderLinesId",
                        column: x => x.OrderLinesId,
                        principalTable: "OrderLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderLineProductPropertyItem_ProductPropertyItems_PropertyItemsId",
                        column: x => x.PropertyItemsId,
                        principalTable: "ProductPropertyItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductProductPropertyItem",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    PropertyItemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductPropertyItem", x => new { x.ProductsId, x.PropertyItemsId });
                    table.ForeignKey(
                        name: "FK_ProductProductPropertyItem_ProductPropertyItems_PropertyItemsId",
                        column: x => x.PropertyItemsId,
                        principalTable: "ProductPropertyItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductPropertyItem_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineProductPropertyItem_PropertyItemsId",
                table: "OrderLineProductPropertyItem",
                column: "PropertyItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductProperty_PropertiesId",
                table: "ProductProductProperty",
                column: "PropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductPropertyItem_PropertyItemsId",
                table: "ProductProductPropertyItem",
                column: "PropertyItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPropertyItems_TheFileTagId",
                table: "ProductPropertyItems",
                column: "TheFileTagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPropertyItems_TheProductPropertyId",
                table: "ProductPropertyItems",
                column: "TheProductPropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLineProductPropertyItem");

            migrationBuilder.DropTable(
                name: "ProductProductProperty");

            migrationBuilder.DropTable(
                name: "ProductProductPropertyItem");

            migrationBuilder.DropTable(
                name: "ProductUnitTypes");

            migrationBuilder.DropTable(
                name: "ProductPropertyItems");

            migrationBuilder.DropTable(
                name: "ProductProperties");
        }
    }
}
