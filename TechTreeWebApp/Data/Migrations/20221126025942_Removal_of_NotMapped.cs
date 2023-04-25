using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechTreeWebApp.Data.Migrations
{
    public partial class Removal_of_NotMapped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatItemId",
                table: "Content",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatItemId",
                table: "Content");
        }
    }
}
