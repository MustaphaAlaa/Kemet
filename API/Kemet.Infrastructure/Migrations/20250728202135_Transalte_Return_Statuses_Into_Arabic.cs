using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kemet.Intrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Transalte_Return_Statuses_Into_Arabic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ReturnStatuses",
                keyColumn: "ReturnStatusId",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "المندوب استلم المنتجات المرتجعة.", "عند شركة الشحن" });

            migrationBuilder.UpdateData(
                table: "ReturnStatuses",
                keyColumn: "ReturnStatusId",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "شركة الشحن في طريقها لإرجاع المنتجات.", "في الطريق" });

            migrationBuilder.UpdateData(
                table: "ReturnStatuses",
                keyColumn: "ReturnStatusId",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "تم استلام المنتجات فعليًا في مكان العمل.", "تم الاستلام" });

            migrationBuilder.UpdateData(
                table: "ReturnStatuses",
                keyColumn: "ReturnStatusId",
                keyValue: 4,
                columns: new[] { "Description", "Name" },
                values: new object[] { "يتم الآن فحص حالة المنتج المرتجع.", "قيد الفحص" });

            migrationBuilder.UpdateData(
                table: "ReturnStatuses",
                keyColumn: "ReturnStatusId",
                keyValue: 5,
                columns: new[] { "Description", "Name" },
                values: new object[] { "تم إرجاع المنتج إلى المخزون.", "تمت إعادة التخزين" });

            migrationBuilder.UpdateData(
                table: "ReturnStatuses",
                keyColumn: "ReturnStatusId",
                keyValue: 6,
                columns: new[] { "Description", "Name" },
                values: new object[] { "تم إتلاف المنتج المرتجع.", "تم الإتلاف" });

            migrationBuilder.UpdateData(
                table: "ReturnStatuses",
                keyColumn: "ReturnStatusId",
                keyValue: 7,
                columns: new[] { "Description", "Name" },
                values: new object[] { "تم فقدان المنتج المرتجع.", "فُقد" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ReturnStatuses",
                keyColumn: "ReturnStatusId",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Delivery person has the items", "With Delivery Company" });

            migrationBuilder.UpdateData(
                table: "ReturnStatuses",
                keyColumn: "ReturnStatusId",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Delivery company bringing items back", "In Transit" });

            migrationBuilder.UpdateData(
                table: "ReturnStatuses",
                keyColumn: "ReturnStatusId",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Items physically returned to the business.", "Received" });

            migrationBuilder.UpdateData(
                table: "ReturnStatuses",
                keyColumn: "ReturnStatusId",
                keyValue: 4,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Checking item condition.", "Under Inspection" });

            migrationBuilder.UpdateData(
                table: "ReturnStatuses",
                keyColumn: "ReturnStatusId",
                keyValue: 5,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Return item is restocked.", "Restocked" });

            migrationBuilder.UpdateData(
                table: "ReturnStatuses",
                keyColumn: "ReturnStatusId",
                keyValue: 6,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Return item is disposed.", "Disposed" });

            migrationBuilder.UpdateData(
                table: "ReturnStatuses",
                keyColumn: "ReturnStatusId",
                keyValue: 7,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Return item is lost.", "Lost" });
        }
    }
}
