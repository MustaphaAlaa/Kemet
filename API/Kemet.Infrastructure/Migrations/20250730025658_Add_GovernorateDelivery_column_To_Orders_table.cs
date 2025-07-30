using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kemet.Intrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_GovernorateDelivery_column_To_Orders_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GovernorateDeliveryId",
                table: "Orders",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_GovernorateDeliveryId",
                table: "Orders",
                column: "GovernorateDeliveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_GovernorateDelivery_GovernorateDeliveryId",
                table: "Orders",
                column: "GovernorateDeliveryId",
                principalTable: "GovernorateDelivery",
                principalColumn: "GovernorateDeliveryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_GovernorateDelivery_GovernorateDeliveryId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_GovernorateDeliveryId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "GovernorateDeliveryId",
                table: "Orders");
        }
    }
}
