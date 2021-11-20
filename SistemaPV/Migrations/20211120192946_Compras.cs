using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaPV.Migrations
{
    public partial class Compras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Managers_ManagerId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ManagerId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Purchases");

            migrationBuilder.AddColumn<int>(
                name: "CManagerId",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Purchases",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CManagerId",
                table: "Purchases",
                column: "CManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_UserId",
                table: "Purchases",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Managers_CManagerId",
                table: "Purchases",
                column: "CManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_AspNetUsers_UserId",
                table: "Purchases",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Managers_CManagerId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_AspNetUsers_UserId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_CManagerId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_UserId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "CManagerId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Purchases");

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ManagerId",
                table: "Purchases",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Managers_ManagerId",
                table: "Purchases",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
