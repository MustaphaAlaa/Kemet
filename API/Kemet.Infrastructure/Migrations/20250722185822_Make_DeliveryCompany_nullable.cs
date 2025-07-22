using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kemet.Intrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Make_DeliveryCompany_nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryCompanies_DeliveryCompanyId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_GovernorateDeliveryCompanies_GovernorateDeliveryComp~",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "GovernorateDeliveryCompanyId",
                table: "Orders",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryCompanyId",
                table: "Orders",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryCompanies_DeliveryCompanyId",
                table: "Orders",
                column: "DeliveryCompanyId",
                principalTable: "DeliveryCompanies",
                principalColumn: "DeliveryCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_GovernorateDeliveryCompanies_GovernorateDeliveryComp~",
                table: "Orders",
                column: "GovernorateDeliveryCompanyId",
                principalTable: "GovernorateDeliveryCompanies",
                principalColumn: "GovernorateDeliveryCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryCompanies_DeliveryCompanyId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_GovernorateDeliveryCompanies_GovernorateDeliveryComp~",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "GovernorateDeliveryCompanyId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryCompanyId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryCompanies_DeliveryCompanyId",
                table: "Orders",
                column: "DeliveryCompanyId",
                principalTable: "DeliveryCompanies",
                principalColumn: "DeliveryCompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_GovernorateDeliveryCompanies_GovernorateDeliveryComp~",
                table: "Orders",
                column: "GovernorateDeliveryCompanyId",
                principalTable: "GovernorateDeliveryCompanies",
                principalColumn: "GovernorateDeliveryCompanyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
