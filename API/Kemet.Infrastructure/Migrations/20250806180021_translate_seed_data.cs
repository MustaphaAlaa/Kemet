using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kemet.Intrastructure.Migrations
{
    /// <inheritdoc />
    public partial class translate_seed_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OrderReceiptStatuses",
                keyColumn: "OrderReceiptStatusId",
                keyValue: 1,
                column: "Name",
                value: "تم الاستلام بالكامل");

            migrationBuilder.UpdateData(
                table: "OrderReceiptStatuses",
                keyColumn: "OrderReceiptStatusId",
                keyValue: 2,
                column: "Name",
                value: "تم الاستلام جزئيًا");

            migrationBuilder.UpdateData(
                table: "OrderReceiptStatuses",
                keyColumn: "OrderReceiptStatusId",
                keyValue: 3,
                column: "Name",
                value: "تم رفض الاستلام");

            migrationBuilder.UpdateData(
                table: "OrderReceiptStatuses",
                keyColumn: "OrderReceiptStatusId",
                keyValue: 4,
                column: "Name",
                value: "فشل محاولة التسليم");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OrderReceiptStatuses",
                keyColumn: "OrderReceiptStatusId",
                keyValue: 1,
                column: "Name",
                value: "Fully Receipt");

            migrationBuilder.UpdateData(
                table: "OrderReceiptStatuses",
                keyColumn: "OrderReceiptStatusId",
                keyValue: 2,
                column: "Name",
                value: "Partially Receipt");

            migrationBuilder.UpdateData(
                table: "OrderReceiptStatuses",
                keyColumn: "OrderReceiptStatusId",
                keyValue: 3,
                column: "Name",
                value: "Refused Receipt");

            migrationBuilder.UpdateData(
                table: "OrderReceiptStatuses",
                keyColumn: "OrderReceiptStatusId",
                keyValue: 4,
                column: "Name",
                value: "Attempt Failed");
        }
    }
}
