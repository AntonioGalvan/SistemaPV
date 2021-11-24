using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaPV.Migrations
{
    public partial class MngrUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Managers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
