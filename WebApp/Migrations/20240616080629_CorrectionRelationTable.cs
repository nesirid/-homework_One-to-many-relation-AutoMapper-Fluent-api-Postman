using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class CorrectionRelationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ArchiveCategories_ArchiveCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ArchiveCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ArchiveCategoryId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ArchiveProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ArchiveCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchiveProducts_ArchiveCategories_ArchiveCategoryId",
                        column: x => x.ArchiveCategoryId,
                        principalTable: "ArchiveCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchiveProducts_ArchiveCategoryId",
                table: "ArchiveProducts",
                column: "ArchiveCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchiveProducts");

            migrationBuilder.AddColumn<int>(
                name: "ArchiveCategoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ArchiveCategoryId",
                table: "Products",
                column: "ArchiveCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ArchiveCategories_ArchiveCategoryId",
                table: "Products",
                column: "ArchiveCategoryId",
                principalTable: "ArchiveCategories",
                principalColumn: "Id");
        }
    }
}
