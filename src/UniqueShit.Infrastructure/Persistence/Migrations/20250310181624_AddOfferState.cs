using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniqueShit.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOfferState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OfferStateId",
                table: "Offer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OfferState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferState", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "OfferState",
                columns: ["Id", "Name"],
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Expired" },
                    { 3, "Completed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offer_OfferStateId",
                table: "Offer",
                column: "OfferStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_OfferState_OfferStateId",
                table: "Offer",
                column: "OfferStateId",
                principalTable: "OfferState",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offer_OfferState_OfferStateId",
                table: "Offer");

            migrationBuilder.DropTable(
                name: "OfferState");

            migrationBuilder.DropIndex(
                name: "IX_Offer_OfferStateId",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "OfferStateId",
                table: "Offer");
        }
    }
}
