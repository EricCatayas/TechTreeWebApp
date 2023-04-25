using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechTreeWebApp.Data.Migrations
{
    public partial class mssqlazure_migration_160 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatItemId",
                table: "Content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatItemId",
                table: "Content",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
