using Microsoft.EntityFrameworkCore.Migrations;

namespace Fashionista.Persistence.Migrations
{
    public partial class ShoppingCartProductModification1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartProducts_ColorId",
                table: "ShoppingCartProducts",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartProducts_SizeId",
                table: "ShoppingCartProducts",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartProducts_ProductColors_ColorId",
                table: "ShoppingCartProducts",
                column: "ColorId",
                principalTable: "ProductColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartProducts_ProductSizes_SizeId",
                table: "ShoppingCartProducts",
                column: "SizeId",
                principalTable: "ProductSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartProducts_ProductColors_ColorId",
                table: "ShoppingCartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartProducts_ProductSizes_SizeId",
                table: "ShoppingCartProducts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartProducts_ColorId",
                table: "ShoppingCartProducts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartProducts_SizeId",
                table: "ShoppingCartProducts");
        }
    }
}
