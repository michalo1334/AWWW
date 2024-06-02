using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCart7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ShoppingCartModel_ShoppingCartId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ShoppingCartModel_ShoppingCartId",
                table: "AspNetUsers",
                column: "ShoppingCartId",
                principalTable: "ShoppingCartModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ShoppingCartModel_ShoppingCartId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ShoppingCartModel_ShoppingCartId",
                table: "AspNetUsers",
                column: "ShoppingCartId",
                principalTable: "ShoppingCartModel",
                principalColumn: "Id");
        }
    }
}
