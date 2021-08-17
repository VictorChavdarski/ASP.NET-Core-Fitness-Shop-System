using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessShopSystem.Migrations
{
    public partial class ProgramImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Products_ProductId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_ProductId",
                table: "Deliveries");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Programs");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_ProductId",
                table: "Deliveries",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Products_ProductId",
                table: "Deliveries",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
