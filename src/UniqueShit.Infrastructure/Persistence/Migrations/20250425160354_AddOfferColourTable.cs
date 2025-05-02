using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniqueShit.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOfferColourTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OfferColour",
                columns: table => new
                {
                    ColourId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferColour", x => new { x.ColourId, x.OfferId });
                    table.ForeignKey(
                        name: "FK_OfferColour_Colour_ColourId",
                        column: x => x.ColourId,
                        principalTable: "Colour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferColour_Offer_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferColour_OfferId",
                table: "OfferColour",
                column: "OfferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferColour");
        }
    }
}
