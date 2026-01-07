using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiscalFlow.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateSatCatalogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SatCatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CfdiVersion = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SatCatalog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SatCatalogRule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CatalogCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ItemKey = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AppliesToCatalog = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppliesToKey = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsAllowed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SatCatalogRule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SatCatalogItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SatCatalogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KeyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SatCatalogItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SatCatalogItem_SatCatalog_SatCatalogId",
                        column: x => x.SatCatalogId,
                        principalTable: "SatCatalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SatCatalog_Code",
                table: "SatCatalog",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SatCatalogItem_KeyCode_StartDate_EndDate",
                table: "SatCatalogItem",
                columns: new[] { "KeyCode", "StartDate", "EndDate" });

            migrationBuilder.CreateIndex(
                name: "IX_SatCatalogItem_SatCatalogId_KeyCode",
                table: "SatCatalogItem",
                columns: new[] { "SatCatalogId", "KeyCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SatCatalogRule_CatalogCode_ItemKey_AppliesToCatalog_AppliesToKey",
                table: "SatCatalogRule",
                columns: new[] { "CatalogCode", "ItemKey", "AppliesToCatalog", "AppliesToKey" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SatCatalogItem");

            migrationBuilder.DropTable(
                name: "SatCatalogRule");

            migrationBuilder.DropTable(
                name: "SatCatalog");
        }
    }
}
