using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechTreeWebApp.Data.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_CategoryItem_categoryItemId",
                table: "Content");

            migrationBuilder.RenameColumn(
                name: "categoryItemId",
                table: "Content",
                newName: "CategoryItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Content_categoryItemId",
                table: "Content",
                newName: "IX_Content_CategoryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_CategoryItem_CategoryItemId",
                table: "Content",
                column: "CategoryItemId",
                principalTable: "CategoryItem",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_CategoryItem_CategoryItemId",
                table: "Content");

            migrationBuilder.RenameColumn(
                name: "CategoryItemId",
                table: "Content",
                newName: "categoryItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Content_CategoryItemId",
                table: "Content",
                newName: "IX_Content_categoryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_CategoryItem_categoryItemId",
                table: "Content",
                column: "categoryItemId",
                principalTable: "CategoryItem",
                principalColumn: "Id");
        }
    }
}
