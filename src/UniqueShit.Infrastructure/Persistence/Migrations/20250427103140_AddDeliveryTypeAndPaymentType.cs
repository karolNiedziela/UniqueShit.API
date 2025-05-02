using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniqueShit.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliveryTypeAndPaymentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryTypeId",
                table: "Offer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentTypeId",
                table: "Offer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DeliveryType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DeliveryType",
                columns: ["Id", "Name"],
                values: new object[,]
                {
                    { 1, "Meeting" },
                    { 2, "Shipping" },
                    { 3, "Any" }
                });

            migrationBuilder.InsertData(
                table: "PaymentType",
                columns: ["Id", "Name"],
                values: new object[,]
                {
                    { 1, "Bank transfer" },
                    { 2, "Cash" },
                    { 3, "Blik" },
                    { 4, "Any" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offer_DeliveryTypeId",
                table: "Offer",
                column: "DeliveryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_PaymentTypeId",
                table: "Offer",
                column: "PaymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_DeliveryType_DeliveryTypeId",
                table: "Offer",
                column: "DeliveryTypeId",
                principalTable: "DeliveryType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_PaymentType_PaymentTypeId",
                table: "Offer",
                column: "PaymentTypeId",
                principalTable: "PaymentType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offer_DeliveryType_DeliveryTypeId",
                table: "Offer");

            migrationBuilder.DropForeignKey(
                name: "FK_Offer_PaymentType_PaymentTypeId",
                table: "Offer");

            migrationBuilder.DropTable(
                name: "DeliveryType");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropIndex(
                name: "IX_Offer_DeliveryTypeId",
                table: "Offer");

            migrationBuilder.DropIndex(
                name: "IX_Offer_PaymentTypeId",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "DeliveryTypeId",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                table: "Offer");
        }
    }
}
