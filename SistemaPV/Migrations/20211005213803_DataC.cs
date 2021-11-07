using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaPV.Migrations
{
    public partial class DataC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CManager_AspNetUsers_CUserId",
                table: "CManager");

            migrationBuilder.DropForeignKey(
                name: "FK_CPurchase_CManager_ManagerId",
                table: "CPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_CPurchaseDetail_Products_ProductId",
                table: "CPurchaseDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_CPurchaseDetail_CPurchase_PurchaseId",
                table: "CPurchaseDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_CSalesman_AspNetUsers_UserId",
                table: "CSalesman");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_CSalesman_SalesmanId",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CSalesman",
                table: "CSalesman");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CPurchaseDetail",
                table: "CPurchaseDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CPurchase",
                table: "CPurchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CManager",
                table: "CManager");

            migrationBuilder.DropColumn(
                name: "Job",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "User",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "CSalesman",
                newName: "Salesmen");

            migrationBuilder.RenameTable(
                name: "CPurchaseDetail",
                newName: "PurchaseDetails");

            migrationBuilder.RenameTable(
                name: "CPurchase",
                newName: "Purchases");

            migrationBuilder.RenameTable(
                name: "CManager",
                newName: "Managers");

            migrationBuilder.RenameIndex(
                name: "IX_CSalesman_UserId",
                table: "Salesmen",
                newName: "IX_Salesmen_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CPurchaseDetail_PurchaseId",
                table: "PurchaseDetails",
                newName: "IX_PurchaseDetails_PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_CPurchaseDetail_ProductId",
                table: "PurchaseDetails",
                newName: "IX_PurchaseDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CPurchase_ManagerId",
                table: "Purchases",
                newName: "IX_Purchases_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_CManager_CUserId",
                table: "Managers",
                newName: "IX_Managers_CUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salesmen",
                table: "Salesmen",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseDetails",
                table: "PurchaseDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Managers",
                table: "Managers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_CUserId",
                        column: x => x.CUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_CUserId",
                table: "Admins",
                column: "CUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_AspNetUsers_CUserId",
                table: "Managers",
                column: "CUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetails_Products_ProductId",
                table: "PurchaseDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetails_Purchases_PurchaseId",
                table: "PurchaseDetails",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Managers_ManagerId",
                table: "Purchases",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Salesmen_SalesmanId",
                table: "Sales",
                column: "SalesmanId",
                principalTable: "Salesmen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salesmen_AspNetUsers_UserId",
                table: "Salesmen",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_AspNetUsers_CUserId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetails_Products_ProductId",
                table: "PurchaseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetails_Purchases_PurchaseId",
                table: "PurchaseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Managers_ManagerId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Salesmen_SalesmanId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Salesmen_AspNetUsers_UserId",
                table: "Salesmen");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Salesmen",
                table: "Salesmen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseDetails",
                table: "PurchaseDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Managers",
                table: "Managers");

            migrationBuilder.RenameTable(
                name: "Salesmen",
                newName: "CSalesman");

            migrationBuilder.RenameTable(
                name: "Purchases",
                newName: "CPurchase");

            migrationBuilder.RenameTable(
                name: "PurchaseDetails",
                newName: "CPurchaseDetail");

            migrationBuilder.RenameTable(
                name: "Managers",
                newName: "CManager");

            migrationBuilder.RenameIndex(
                name: "IX_Salesmen_UserId",
                table: "CSalesman",
                newName: "IX_CSalesman_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_ManagerId",
                table: "CPurchase",
                newName: "IX_CPurchase_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetails_PurchaseId",
                table: "CPurchaseDetail",
                newName: "IX_CPurchaseDetail_PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetails_ProductId",
                table: "CPurchaseDetail",
                newName: "IX_CPurchaseDetail_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Managers_CUserId",
                table: "CManager",
                newName: "IX_CManager_CUserId");

            migrationBuilder.AddColumn<string>(
                name: "Job",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CSalesman",
                table: "CSalesman",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CPurchase",
                table: "CPurchase",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CPurchaseDetail",
                table: "CPurchaseDetail",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CManager",
                table: "CManager",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CManager_AspNetUsers_CUserId",
                table: "CManager",
                column: "CUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CPurchase_CManager_ManagerId",
                table: "CPurchase",
                column: "ManagerId",
                principalTable: "CManager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CPurchaseDetail_Products_ProductId",
                table: "CPurchaseDetail",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CPurchaseDetail_CPurchase_PurchaseId",
                table: "CPurchaseDetail",
                column: "PurchaseId",
                principalTable: "CPurchase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CSalesman_AspNetUsers_UserId",
                table: "CSalesman",
                column: "UserId",
                principalTable: "AspNetUsers",
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
    }
}
