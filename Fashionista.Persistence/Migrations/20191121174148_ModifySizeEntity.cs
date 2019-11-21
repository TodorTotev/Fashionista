using Microsoft.EntityFrameworkCore.Migrations;

namespace Fashionista.Persistence.Migrations
{
    public partial class ModifySizeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductColor_ProductColorId",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductSize_ProductSizeId",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductColor_ProductColorId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductSize_ProductSizeId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSize",
                table: "ProductSize");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductColor",
                table: "ProductColor");

            migrationBuilder.RenameTable(
                name: "ProductSize",
                newName: "ProductSizes");

            migrationBuilder.RenameTable(
                name: "ProductColor",
                newName: "ProductColors");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSize_IsDeleted",
                table: "ProductSizes",
                newName: "IX_ProductSizes_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_ProductColor_IsDeleted",
                table: "ProductColors",
                newName: "IX_ProductColors_IsDeleted");

            migrationBuilder.AddColumn<int>(
                name: "MainCategoryId",
                table: "ProductSizes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSizes",
                table: "ProductSizes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_MainCategoryId",
                table: "ProductSizes",
                column: "MainCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_ProductColors_ProductColorId",
                table: "ProductAttributes",
                column: "ProductColorId",
                principalTable: "ProductColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_ProductSizes_ProductSizeId",
                table: "ProductAttributes",
                column: "ProductSizeId",
                principalTable: "ProductSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_MainCategories_MainCategoryId",
                table: "ProductSizes",
                column: "MainCategoryId",
                principalTable: "MainCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductColors_ProductColorId",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductSizes_ProductSizeId",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductColors_ProductColorId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductSizes_ProductSizeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_MainCategories_MainCategoryId",
                table: "ProductSizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSizes",
                table: "ProductSizes");

            migrationBuilder.DropIndex(
                name: "IX_ProductSizes_MainCategoryId",
                table: "ProductSizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors");

            migrationBuilder.DropColumn(
                name: "MainCategoryId",
                table: "ProductSizes");

            migrationBuilder.RenameTable(
                name: "ProductSizes",
                newName: "ProductSize");

            migrationBuilder.RenameTable(
                name: "ProductColors",
                newName: "ProductColor");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizes_IsDeleted",
                table: "ProductSize",
                newName: "IX_ProductSize_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_ProductColors_IsDeleted",
                table: "ProductColor",
                newName: "IX_ProductColor_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSize",
                table: "ProductSize",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductColor",
                table: "ProductColor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_ProductColor_ProductColorId",
                table: "ProductAttributes",
                column: "ProductColorId",
                principalTable: "ProductColor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_ProductSize_ProductSizeId",
                table: "ProductAttributes",
                column: "ProductSizeId",
                principalTable: "ProductSize",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductColor_ProductColorId",
                table: "Products",
                column: "ProductColorId",
                principalTable: "ProductColor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductSize_ProductSizeId",
                table: "Products",
                column: "ProductSizeId",
                principalTable: "ProductSize",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
