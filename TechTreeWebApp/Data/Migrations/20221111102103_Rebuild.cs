using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechTreeWebApp.Data.Migrations
{
    public partial class Rebuild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItem_MediaType_MediaTypeId",
                table: "CategoryItem");

            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "CategoryItem");

            migrationBuilder.AlterColumn<int>(
                name: "MediaTypeId",
                table: "CategoryItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItem_MediaType_MediaTypeId",
                table: "CategoryItem",
                column: "MediaTypeId",
                principalTable: "MediaType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItem_MediaType_MediaTypeId",
                table: "CategoryItem");

            migrationBuilder.AlterColumn<int>(
                name: "MediaTypeId",
                table: "CategoryItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MediaType",
                table: "CategoryItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItem_MediaType_MediaTypeId",
                table: "CategoryItem",
                column: "MediaTypeId",
                principalTable: "MediaType",
                principalColumn: "Id");
        }
    }
}
