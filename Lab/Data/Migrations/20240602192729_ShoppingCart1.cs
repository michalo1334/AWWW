using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCart1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ShoppingCartModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartModel_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItemModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    shoppingCartId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItemModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItemModel_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItemModel_ShoppingCartModel_shoppingCartId",
                        column: x => x.shoppingCartId,
                        principalTable: "ShoppingCartModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShoppingCartId",
                table: "AspNetUsers",
                column: "ShoppingCartId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItemModel_ProductId",
                table: "ShoppingCartItemModel",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItemModel_shoppingCartId",
                table: "ShoppingCartItemModel",
                column: "shoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartModel_UserId",
                table: "ShoppingCartModel",
                column: "UserId");

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

            migrationBuilder.DropTable(
                name: "ShoppingCartItemModel");

            migrationBuilder.DropTable(
                name: "ShoppingCartModel");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShoppingCartId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "AspNetUsers");
        }
    }
}
