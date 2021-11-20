using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaPV.Migrations
{
    public partial class CSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Salesmen_SalesmanId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_SalesmanId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SalesmanId",
                table: "Sales");

            migrationBuilder.AddColumn<int>(
                name: "CSalesmanId",
                table: "Sales",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Sales",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CSalesmanId",
                table: "Sales",
                column: "CSalesmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_UserId",
                table: "Sales",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Salesmen_CSalesmanId",
                table: "Sales",
                column: "CSalesmanId",
                principalTable: "Salesmen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_AspNetUsers_UserId",
                table: "Sales",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Salesmen_CSalesmanId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_AspNetUsers_UserId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_CSalesmanId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_UserId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CSalesmanId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sales");

            migrationBuilder.AddColumn<int>(
                name: "SalesmanId",
                table: "Sales",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SalesmanId",
                table: "Sales",
                column: "SalesmanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Salesmen_SalesmanId",
                table: "Sales",
                column: "SalesmanId",
                principalTable: "Salesmen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
