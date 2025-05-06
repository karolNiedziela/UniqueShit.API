using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniqueShit.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeparateSaleAndPurchaseOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteOffer_Offer_OfferId",
                table: "FavouriteOffer");

            migrationBuilder.DropTable(
                name: "OfferColour");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropTable(
                name: "OfferType");

            migrationBuilder.RenameColumn(
                name: "OfferId",
                table: "FavouriteOffer",
                newName: "SaleOfferId");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteOffer_OfferId",
                table: "FavouriteOffer",
                newName: "IX_FavouriteOffer_SaleOfferId");

            migrationBuilder.CreateTable(
                name: "PurchaseOffer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfferStateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOffer_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "ADObjectId");
                    table.ForeignKey(
                        name: "FK_PurchaseOffer_Model_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Model",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOffer_OfferState_OfferStateId",
                        column: x => x.OfferStateId,
                        principalTable: "OfferState",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SaleOffer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ItemConditionId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    DeliveryTypeId = table.Column<int>(type: "int", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfferStateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOffer_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "ADObjectId");
                    table.ForeignKey(
                        name: "FK_SaleOffer_DeliveryType_DeliveryTypeId",
                        column: x => x.DeliveryTypeId,
                        principalTable: "DeliveryType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleOffer_ItemCondition_ItemConditionId",
                        column: x => x.ItemConditionId,
                        principalTable: "ItemCondition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleOffer_Model_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Model",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleOffer_OfferState_OfferStateId",
                        column: x => x.OfferStateId,
                        principalTable: "OfferState",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleOffer_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleOffer_Size_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Size",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SaleOfferColour",
                columns: table => new
                {
                    ColourId = table.Column<int>(type: "int", nullable: false),
                    SaleOfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOfferColour", x => new { x.ColourId, x.SaleOfferId });
                    table.ForeignKey(
                        name: "FK_SaleOfferColour_Colour_ColourId",
                        column: x => x.ColourId,
                        principalTable: "Colour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleOfferColour_SaleOffer_SaleOfferId",
                        column: x => x.SaleOfferId,
                        principalTable: "SaleOffer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOffer_AppUserId",
                table: "PurchaseOffer",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOffer_ModelId",
                table: "PurchaseOffer",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOffer_OfferStateId",
                table: "PurchaseOffer",
                column: "OfferStateId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOffer_AppUserId",
                table: "SaleOffer",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOffer_DeliveryTypeId",
                table: "SaleOffer",
                column: "DeliveryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOffer_ItemConditionId",
                table: "SaleOffer",
                column: "ItemConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOffer_ModelId",
                table: "SaleOffer",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOffer_OfferStateId",
                table: "SaleOffer",
                column: "OfferStateId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOffer_PaymentTypeId",
                table: "SaleOffer",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOffer_SizeId",
                table: "SaleOffer",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOfferColour_SaleOfferId",
                table: "SaleOfferColour",
                column: "SaleOfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteOffer_SaleOffer_SaleOfferId",
                table: "FavouriteOffer",
                column: "SaleOfferId",
                principalTable: "SaleOffer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteOffer_SaleOffer_SaleOfferId",
                table: "FavouriteOffer");

            migrationBuilder.DropTable(
                name: "PurchaseOffer");

            migrationBuilder.DropTable(
                name: "SaleOfferColour");

            migrationBuilder.DropTable(
                name: "SaleOffer");

            migrationBuilder.RenameColumn(
                name: "SaleOfferId",
                table: "FavouriteOffer",
                newName: "OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteOffer_SaleOfferId",
                table: "FavouriteOffer",
                newName: "IX_FavouriteOffer_OfferId");

            migrationBuilder.CreateTable(
                name: "OfferType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryTypeId = table.Column<int>(type: "int", nullable: false),
                    ExpiredAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ItemConditionId = table.Column<int>(type: "int", nullable: false),
                    OfferStateId = table.Column<int>(type: "int", nullable: false),
                    OfferTypeId = table.Column<int>(type: "int", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offer_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "ADObjectId");
                    table.ForeignKey(
                        name: "FK_Offer_DeliveryType_DeliveryTypeId",
                        column: x => x.DeliveryTypeId,
                        principalTable: "DeliveryType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offer_ItemCondition_ItemConditionId",
                        column: x => x.ItemConditionId,
                        principalTable: "ItemCondition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offer_Model_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Model",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offer_OfferState_OfferStateId",
                        column: x => x.OfferStateId,
                        principalTable: "OfferState",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offer_OfferType_OfferTypeId",
                        column: x => x.OfferTypeId,
                        principalTable: "OfferType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offer_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offer_Size_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Size",
                        principalColumn: "Id");
                });

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

            migrationBuilder.InsertData(
                table: "OfferType",
                columns: ["Id", "Name"],
                values: new object[,]
                {
                    { 1, "Purchase" },
                    { 2, "Sale" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offer_AppUserId",
                table: "Offer",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_DeliveryTypeId",
                table: "Offer",
                column: "DeliveryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_ItemConditionId",
                table: "Offer",
                column: "ItemConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_ModelId",
                table: "Offer",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_OfferStateId",
                table: "Offer",
                column: "OfferStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_OfferTypeId",
                table: "Offer",
                column: "OfferTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_PaymentTypeId",
                table: "Offer",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_SizeId",
                table: "Offer",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferColour_OfferId",
                table: "OfferColour",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteOffer_Offer_OfferId",
                table: "FavouriteOffer",
                column: "OfferId",
                principalTable: "Offer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
