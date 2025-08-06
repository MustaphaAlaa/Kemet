using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kemet.Intrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix_OrderReceiptStatusId1_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderReceiptStatuses_OrderReceiptStatuses_OrderReceiptStatu~",
                table: "OrderReceiptStatuses");

            migrationBuilder.DropIndex(
                name: "IX_OrderReceiptStatuses_OrderReceiptStatusId1",
                table: "OrderReceiptStatuses");

            migrationBuilder.DropColumn(
                name: "OrderReceiptStatusId1",
                table: "OrderReceiptStatuses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderReceiptStatusId1",
                table: "OrderReceiptStatuses",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "OrderReceiptStatuses",
                keyColumn: "OrderReceiptStatusId",
                keyValue: 1,
                column: "OrderReceiptStatusId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "OrderReceiptStatuses",
                keyColumn: "OrderReceiptStatusId",
                keyValue: 2,
                column: "OrderReceiptStatusId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "OrderReceiptStatuses",
                keyColumn: "OrderReceiptStatusId",
                keyValue: 3,
                column: "OrderReceiptStatusId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "OrderReceiptStatuses",
                keyColumn: "OrderReceiptStatusId",
                keyValue: 4,
                column: "OrderReceiptStatusId1",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_OrderReceiptStatuses_OrderReceiptStatusId1",
                table: "OrderReceiptStatuses",
                column: "OrderReceiptStatusId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderReceiptStatuses_OrderReceiptStatuses_OrderReceiptStatu~",
                table: "OrderReceiptStatuses",
                column: "OrderReceiptStatusId1",
                principalTable: "OrderReceiptStatuses",
                principalColumn: "OrderReceiptStatusId");
        }
    }
}
