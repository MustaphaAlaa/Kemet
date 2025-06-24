using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kemet.Intrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Governorate_SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Governorates",
                columns: new[] { "GovernorateId", "IsAvailableToDeliver", "Name" },
                values: new object[,]
                {
                    { 1, true, "القاهرة" },
                    { 2, true, "الجيزة" },
                    { 3, true, "الإسكندرية" },
                    { 4, true, "الدقهلية" },
                    { 5, true, "البحر الأحمر" },
                    { 6, true, "البحيرة" },
                    { 7, true, "الفيوم" },
                    { 8, true, "الغربية" },
                    { 9, true, "الشرقية" },
                    { 10, true, "الإسماعيلية" },
                    { 11, true, "المنوفية" },
                    { 12, true, "المنيا" },
                    { 13, true, "القليوبية" },
                    { 14, true, "الوادي الجديد" },
                    { 15, true, "السويس" },
                    { 16, true, "أسوان" },
                    { 17, true, "أسيوط" },
                    { 18, true, "بني سويف" },
                    { 19, true, "بورسعيد" },
                    { 20, true, "دمياط" },
                    { 21, true, "كفر الشيخ" },
                    { 22, true, "مطروح" },
                    { 23, true, "الأقصر" },
                    { 24, true, "قنا" },
                    { 25, true, "سوهاج" },
                    { 26, true, "جنوب سيناء" },
                    { 27, true, "شمال سيناء" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 27);
        }
    }
}
