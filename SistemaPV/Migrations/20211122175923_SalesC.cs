using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaPV.Migrations
{
    public partial class SalesC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CSaleTempId",
                table: "SaleDetailTemps",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CSaleTemps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: true),
                    PaidAmount = table.Column<double>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CSaleTemps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CSaleTemps_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetailTemps_CSaleTempId",
                table: "SaleDetailTemps",
                column: "CSaleTempId");

            migrationBuilder.CreateIndex(
                name: "IX_CSaleTemps_UserId",
                table: "CSaleTemps",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetailTemps_CSaleTemps_CSaleTempId",
                table: "SaleDetailTemps",
                column: "CSaleTempId",
                principalTable: "CSaleTemps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetailTemps_CSaleTemps_CSaleTempId",
                table: "SaleDetailTemps");

            migrationBuilder.DropTable(
                name: "CSaleTemps");

            migrationBuilder.DropIndex(
                name: "IX_SaleDetailTemps_CSaleTempId",
                table: "SaleDetailTemps");

            migrationBuilder.DropColumn(
                name: "CSaleTempId",
                table: "SaleDetailTemps");
        }
    }
}
