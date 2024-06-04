using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCart10 : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartModel_AspNetUsers_UserId",
                table: "ShoppingCartModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartModel",
                table: "ShoppingCartModel");

            migrationBuilder.RenameTable(
                name: "ShoppingCartModel",
                newName: "ShoppingCarts");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartModel_UserId",
                table: "ShoppingCarts",
                newName: "IX_ShoppingCarts_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ShoppingCarts_ShoppingCartId",
                table: "AspNetUsers",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItemModel_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartItemModel",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                principalTable: "AspNetUsers",
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

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_UserId",
                table: "ShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts");

            migrationBuilder.RenameTable(
                name: "ShoppingCarts",
                newName: "ShoppingCartModel");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCartModel",
                newName: "IX_ShoppingCartModel_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartModel_AspNetUsers_UserId",
                table: "ShoppingCartModel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
