using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CPW215_QuarterProject.Data.Migrations
{
    public partial class RemoveInheritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorFullName",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "BookFormat",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Developer",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GameRating",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "VideoGame_Publisher",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "item_type",
                table: "Items",
                newName: "ItemType");

            migrationBuilder.AlterColumn<string>(
                name: "ItemType",
                table: "Items",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemType",
                table: "Items",
                newName: "item_type");

            migrationBuilder.AlterColumn<string>(
                name: "item_type",
                table: "Items",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorFullName",
                table: "Items",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookFormat",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Items",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Developer",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameRating",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoGame_Publisher",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Items",
                type: "datetime2",
                nullable: true);
        }
    }
}
