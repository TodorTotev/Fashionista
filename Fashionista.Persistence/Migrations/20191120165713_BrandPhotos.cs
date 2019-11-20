using Microsoft.EntityFrameworkCore.Migrations;

namespace Fashionista.Persistence.Migrations
{
    public partial class BrandPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Brands_BrandId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_BrandId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Photo");

            migrationBuilder.AddColumn<string>(
                name: "BrandPhotoUrl",
                table: "Brands",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandPhotoUrl",
                table: "Brands");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Photo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_BrandId",
                table: "Photo",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Brands_BrandId",
                table: "Photo",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
