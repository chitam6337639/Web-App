using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Data.Migrations
{
    public partial class Demo_Ver11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProduct_Order");

            migrationBuilder.DropTable(
                name: "ProductProduct_Order");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Orders_OrderId",
                table: "Product_Orders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Orders_ProductId",
                table: "Product_Orders",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Orders_Orders_OrderId",
                table: "Product_Orders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Orders_Products_ProductId",
                table: "Product_Orders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Orders_Orders_OrderId",
                table: "Product_Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Orders_Products_ProductId",
                table: "Product_Orders");

            migrationBuilder.DropIndex(
                name: "IX_Product_Orders_OrderId",
                table: "Product_Orders");

            migrationBuilder.DropIndex(
                name: "IX_Product_Orders_ProductId",
                table: "Product_Orders");

            migrationBuilder.CreateTable(
                name: "OrderProduct_Order",
                columns: table => new
                {
                    OrdersOrderId = table.Column<int>(type: "int", nullable: false),
                    Product_OrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct_Order", x => new { x.OrdersOrderId, x.Product_OrdersId });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Order_Orders_OrdersOrderId",
                        column: x => x.OrdersOrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Order_Product_Orders_Product_OrdersId",
                        column: x => x.Product_OrdersId,
                        principalTable: "Product_Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductProduct_Order",
                columns: table => new
                {
                    Product_OrdersId = table.Column<int>(type: "int", nullable: false),
                    ProductsProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProduct_Order", x => new { x.Product_OrdersId, x.ProductsProductId });
                    table.ForeignKey(
                        name: "FK_ProductProduct_Order_Product_Orders_Product_OrdersId",
                        column: x => x.Product_OrdersId,
                        principalTable: "Product_Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProduct_Order_Products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_Order_Product_OrdersId",
                table: "OrderProduct_Order",
                column: "Product_OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProduct_Order_ProductsProductId",
                table: "ProductProduct_Order",
                column: "ProductsProductId");
        }
    }
}
