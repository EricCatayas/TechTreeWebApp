using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechTreeWebApp.Data.Migrations
{
    public partial class EntityDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_CategoryItem_CategoryItemId",
                table: "Content");

            migrationBuilder.DropIndex(
                name: "IX_Content_CategoryItemId",
                table: "Content");

            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "Content",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryItemId",
                table: "CategoryItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "CategoryItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserCategory_CategoryId",
                table: "UserCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Content_CategoryItemId",
                table: "Content",
                column: "CategoryItemId",
                unique: true,
                filter: "[CategoryItemId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItem_CategoryId",
                table: "CategoryItem",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItem_Category_CategoryId",
                table: "CategoryItem",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Content_CategoryItem_CategoryItemId",
                table: "Content",
                column: "CategoryItemId",
                principalTable: "CategoryItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCategory_Category_CategoryId",
                table: "UserCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItem_Category_CategoryId",
                table: "CategoryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Content_CategoryItem_CategoryItemId",
                table: "Content");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCategory_Category_CategoryId",
                table: "UserCategory");

            migrationBuilder.DropIndex(
                name: "IX_UserCategory_CategoryId",
                table: "UserCategory");

            migrationBuilder.DropIndex(
                name: "IX_Content_CategoryItemId",
                table: "Content");

            migrationBuilder.DropIndex(
                name: "IX_CategoryItem_CategoryId",
                table: "CategoryItem");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "CategoryItemId",
                table: "CategoryItem");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "CategoryItem");

            migrationBuilder.CreateIndex(
                name: "IX_Content_CategoryItemId",
                table: "Content",
                column: "CategoryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_CategoryItem_CategoryItemId",
                table: "Content",
                column: "CategoryItemId",
                principalTable: "CategoryItem",
                principalColumn: "Id");
        }
    }
}
