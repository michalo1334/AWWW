using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCart6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItemModel_ShoppingCartModel_shoppingCartId",
                table: "ShoppingCartItemModel");

            migrationBuilder.RenameColumn(
                name: "shoppingCartId",
                table: "ShoppingCartItemModel",
                newName: "ShoppingCartId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItemModel_shoppingCartId",
                table: "ShoppingCartItemModel",
                newName: "IX_ShoppingCartItemModel_ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItemModel_ShoppingCartModel_ShoppingCartId",
                table: "ShoppingCartItemModel",
                column: "ShoppingCartId",
                principalTable: "ShoppingCartModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItemModel_ShoppingCartModel_ShoppingCartId",
                table: "ShoppingCartItemModel");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartId",
                table: "ShoppingCartItemModel",
                newName: "shoppingCartId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItemModel_ShoppingCartId",
                table: "ShoppingCartItemModel",
                newName: "IX_ShoppingCartItemModel_shoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItemModel_ShoppingCartModel_shoppingCartId",
                table: "ShoppingCartItemModel",
                column: "shoppingCartId",
                principalTable: "ShoppingCartModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
