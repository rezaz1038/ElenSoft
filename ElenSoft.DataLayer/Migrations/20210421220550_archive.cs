using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElenSoft.DataLayer.Migrations
{
    public partial class archive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archives_Tages_TageId",
                table: "Archives");

            migrationBuilder.DropTable(
                name: "Tages");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "EquipmentPlaces",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "DeviceTypes",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "DeviceBrands",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "TageId",
                table: "Archives",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "Descriptiion",
                table: "Archives",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_Archives_TageId",
                table: "Archives",
                newName: "IX_Archives_TagId");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Archives_Tags_TagId",
                table: "Archives",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archives_Tags_TagId",
                table: "Archives");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "EquipmentPlaces",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "DeviceTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "DeviceBrands",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "Archives",
                newName: "TageId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Archives",
                newName: "Descriptiion");

            migrationBuilder.RenameIndex(
                name: "IX_Archives_TagId",
                table: "Archives",
                newName: "IX_Archives_TageId");

            migrationBuilder.CreateTable(
                name: "Tages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tages", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Archives_Tages_TageId",
                table: "Archives",
                column: "TageId",
                principalTable: "Tages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
