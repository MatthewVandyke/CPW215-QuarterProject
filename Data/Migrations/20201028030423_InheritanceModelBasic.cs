using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CPW215_QuarterProject.Data.Migrations
{
    public partial class InheritanceModelBasic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<string>(nullable: false),
                    item_type = table.Column<string>(maxLength: 200, nullable: false),
                    SellerId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    ISBN = table.Column<string>(nullable: true),
                    AuthorFullName = table.Column<string>(maxLength: 50, nullable: true),
                    Publisher = table.Column<string>(maxLength: 50, nullable: true),
                    BookFormat = table.Column<int>(nullable: true),
                    VideoGame_Publisher = table.Column<string>(nullable: true),
                    Developer = table.Column<string>(nullable: true),
                    GameRating = table.Column<int>(nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_AspNetUsers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_SellerId",
                table: "Items",
                column: "SellerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
