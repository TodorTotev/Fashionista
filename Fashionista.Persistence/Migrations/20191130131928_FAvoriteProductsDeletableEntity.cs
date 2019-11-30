using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fashionista.Persistence.Migrations
{
    public partial class FAvoriteProductsDeletableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "FavoriteProducts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FavoriteProducts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProducts_IsDeleted",
                table: "FavoriteProducts",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FavoriteProducts_IsDeleted",
                table: "FavoriteProducts");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "FavoriteProducts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FavoriteProducts");
        }
    }
}
