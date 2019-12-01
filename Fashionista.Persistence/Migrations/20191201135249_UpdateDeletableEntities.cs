using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fashionista.Persistence.Migrations
{
    public partial class UpdateDeletableEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "ShoppingCartProducts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ShoppingCartProducts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "OrderProducts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OrderProducts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartProducts_IsDeleted",
                table: "ShoppingCartProducts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_IsDeleted",
                table: "OrderProducts",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartProducts_IsDeleted",
                table: "ShoppingCartProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_IsDeleted",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "ShoppingCartProducts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ShoppingCartProducts");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OrderProducts");
        }
    }
}
