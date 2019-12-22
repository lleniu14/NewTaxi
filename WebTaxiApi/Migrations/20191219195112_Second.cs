using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DriversOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DriverId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriversOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriversOrders_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriversOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriversOrders_DriverId",
                table: "DriversOrders",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriversOrders_OrderId",
                table: "DriversOrders",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriversOrders");
        }
    }
}
