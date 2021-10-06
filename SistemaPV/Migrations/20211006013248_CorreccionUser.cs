using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaPV.Migrations
{
    public partial class CorreccionUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Salesmen");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Managers");

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Salesmen",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Managers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
