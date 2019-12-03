using Microsoft.EntityFrameworkCore.Migrations;

namespace Fashionista.Persistence.Migrations
{
    public partial class ProductModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductColors_ProductColorId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductSizes_ProductSizeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductColorId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductSizeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductColorId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductSizeId",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductColorId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductSizeId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductColorId",
                table: "Products",
                column: "ProductColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductSizeId",
                table: "Products",
                column: "ProductSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductColors_ProductColorId",
                table: "Products",
                column: "ProductColorId",
                principalTable: "ProductColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductSizes_ProductSizeId",
                table: "Products",
                column: "ProductSizeId",
                principalTable: "ProductSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
