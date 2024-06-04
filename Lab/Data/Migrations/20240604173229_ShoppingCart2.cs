using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCart2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ShoppingCartModel_ShoppingCartId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItemModel_ShoppingCartModel_ShoppingCartId",
                table: "ShoppingCartItemModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartModel",
                table: "ShoppingCartModel");

            migrationBuilder.RenameTable(
                name: "ShoppingCartModel",
                newName: "ShoppingCarts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ShoppingCarts_ShoppingCartId",
                table: "AspNetUsers",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItemModel_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartItemModel",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ShoppingCarts_ShoppingCartId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItemModel_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartItemModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts");

            migrationBuilder.RenameTable(
                name: "ShoppingCarts",
                newName: "ShoppingCartModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartModel",
                table: "ShoppingCartModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ShoppingCartModel_ShoppingCartId",
                table: "AspNetUsers",
                column: "ShoppingCartId",
                principalTable: "ShoppingCartModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItemModel_ShoppingCartModel_ShoppingCartId",
                table: "ShoppingCartItemModel",
                column: "ShoppingCartId",
                principalTable: "ShoppingCartModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
