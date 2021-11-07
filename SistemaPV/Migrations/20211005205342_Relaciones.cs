using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaPV.Migrations
{
    public partial class Relaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SaleDetails_SaleDetailId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId1",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_SaleDetails_SaleDetailId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_AspNetUsers_UserId1",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_SaleDetailId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_UserId1",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Products_SaleDetailId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SaleDetailId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "SalesmanId",
                table: "Sales",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "SaleDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SaleId",
                table: "SaleDetails",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CManager",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(maxLength: 20, nullable: false),
                    CUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CManager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CManager_AspNetUsers_CUserId",
                        column: x => x.CUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CSalesman",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(maxLength: 20, nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CSalesman", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CSalesman_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CPurchase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 30, nullable: false),
                    Received = table.Column<double>(nullable: false),
                    Change = table.Column<double>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    ManagerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPurchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CPurchase_CManager_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "CManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CPurchaseDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<double>(nullable: false),
                    PurchaseId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPurchaseDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CPurchaseDetail_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CPurchaseDetail_CPurchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "CPurchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SalesmanId",
                table: "Sales",
                column: "SalesmanId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_ProductId",
                table: "SaleDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_SaleId",
                table: "SaleDetails",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_CManager_CUserId",
                table: "CManager",
                column: "CUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CPurchase_ManagerId",
                table: "CPurchase",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_CPurchaseDetail_ProductId",
                table: "CPurchaseDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CPurchaseDetail_PurchaseId",
                table: "CPurchaseDetail",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CSalesman_UserId",
                table: "CSalesman",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_Products_ProductId",
                table: "SaleDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_Sales_SaleId",
                table: "SaleDetails",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_CSalesman_SalesmanId",
                table: "Sales",
                column: "SalesmanId",
                principalTable: "CSalesman",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_Products_ProductId",
                table: "SaleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_Sales_SaleId",
                table: "SaleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_CSalesman_SalesmanId",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "CPurchaseDetail");

            migrationBuilder.DropTable(
                name: "CSalesman");

            migrationBuilder.DropTable(
                name: "CPurchase");

            migrationBuilder.DropTable(
                name: "CManager");

            migrationBuilder.DropIndex(
                name: "IX_Sales_SalesmanId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_SaleDetails_ProductId",
                table: "SaleDetails");

            migrationBuilder.DropIndex(
                name: "IX_SaleDetails_SaleId",
                table: "SaleDetails");

            migrationBuilder.DropColumn(
                name: "SalesmanId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SaleDetails");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "SaleDetails");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Sales",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SaleDetailId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SaleDetailId",
                table: "Sales",
                column: "SaleDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_UserId1",
                table: "Sales",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SaleDetailId",
                table: "Products",
                column: "SaleDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId1",
                table: "Products",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SaleDetails_SaleDetailId",
                table: "Products",
                column: "SaleDetailId",
                principalTable: "SaleDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId1",
                table: "Products",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_SaleDetails_SaleDetailId",
                table: "Sales",
                column: "SaleDetailId",
                principalTable: "SaleDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_AspNetUsers_UserId1",
                table: "Sales",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
