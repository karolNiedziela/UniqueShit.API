using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniqueShit.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAnyPaymentTypeAndDeliveryTypeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeliveryType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PaymentType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "DeliveryType",
                columns: ["Id", "Name"],
                values: new object[] { 99, "Any" });

            migrationBuilder.InsertData(
                table: "PaymentType",
                columns: ["Id", "Name"],
                values: new object[] { 99, "Any" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeliveryType",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "PaymentType",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.InsertData(
                table: "DeliveryType",
                columns: ["Id", "Name"],
                values: new object[] { 3, "Any" });

            migrationBuilder.InsertData(
                table: "PaymentType",
                columns: ["Id", "Name"],
                values: new object[] { 4, "Any" });
        }
    }
}
