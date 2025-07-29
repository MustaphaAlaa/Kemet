using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kemet.Intrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixed_dublicated_columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReturnItems_ReturnStatuses_ReturnStatusId1",
                table: "ReturnItems"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Returns_AspNetUsers_HandledByUserId1",
                table: "Returns"
            );

            migrationBuilder.DropIndex(name: "IX_Returns_HandledByUserId1", table: "Returns");

            migrationBuilder.DropIndex(
                name: "IX_ReturnItems_ReturnStatusId1",
                table: "ReturnItems"
            );

            migrationBuilder.DropColumn(name: "HandledByUserId1", table: "Returns");

            migrationBuilder.DropColumn(name: "ReturnStatusId1", table: "ReturnItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "HandledByUserId",
                table: "Returns",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Returns_HandledByUserId",
                table: "Returns",
                column: "HandledByUserId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Returns_AspNetUsers_HandledByUserId",
                table: "Returns",
                column: "HandledByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Returns_AspNetUsers_HandledByUserId",
                table: "Returns"
            );

            migrationBuilder.DropIndex(name: "IX_Returns_HandledByUserId", table: "Returns");

            migrationBuilder.AlterColumn<int>(
                name: "HandledByUserId",
                table: "Returns",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid"
            );

            migrationBuilder.AddColumn<Guid>(
                name: "HandledByUserId1",
                table: "Returns",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000")
            );

            migrationBuilder.AddColumn<int>(
                name: "ReturnStatusId1",
                table: "ReturnItems",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.CreateIndex(
                name: "IX_Returns_HandledByUserId1",
                table: "Returns",
                column: "HandledByUserId1"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ReturnItems_ReturnStatusId1",
                table: "ReturnItems",
                column: "ReturnStatusId1"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnItems_ReturnStatuses_ReturnStatusId1",
                table: "ReturnItems",
                column: "ReturnStatusId1",
                principalTable: "ReturnStatuses",
                principalColumn: "ReturnStatusId",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Returns_AspNetUsers_HandledByUserId1",
                table: "Returns",
                column: "HandledByUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
