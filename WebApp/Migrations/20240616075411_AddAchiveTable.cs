﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class AddAchiveTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArchiveCategoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ArchiveCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveCategories", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ArchiveCategories_ArchiveCategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ArchiveCategories");

            migrationBuilder.DropIndex(
                name: "IX_Products_ArchiveCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ArchiveCategoryId",
                table: "Products");
        }
    }
}
