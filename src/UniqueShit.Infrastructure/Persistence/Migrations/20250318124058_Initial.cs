using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniqueShit.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    ADObjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.ADObjectId)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemCondition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCondition", x => x.Id);
                });

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
                name: "ProductCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Model",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Model_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Model_ProductCategory_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Size_ProductCategory_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfferTypeId = table.Column<int>(type: "int", nullable: false),
                    ItemConditionId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    OfferStateId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                        name: "FK_Offer_Size_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Size",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Colour",
                columns: ["Id", "Name"],
                values: new object[,]
                {
                    { 1, "Black" },
                    { 2, "White" },
                    { 3, "Red" },
                    { 4, "Blue" },
                    { 5, "Green" }
                });

            migrationBuilder.InsertData(
                table: "ItemCondition",
                columns: ["Id", "Name"],
                values: new object[,]
                {
                    { 1, "New" },
                    { 2, "Used" },
                    { 3, "Damaged" }
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

            migrationBuilder.InsertData(
                table: "OfferType",
                columns: ["Id", "Name"],
                values: new object[,]
                {
                    { 1, "Purchase" },
                    { 2, "Sale" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: ["Id", "Name"],
                values: new object[,]
                {
                    { 1, "Shoes" },
                    { 2, "T-shirts" },
                    { 3, "Hoodies" },
                    { 4, "Caps" },
                    { 5, "Socks" },
                    { 6, "Jackets" },
                    { 7, "Tracksuits" },
                    { 8, "Trousers" },
                    { 9, "Underwear" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_ADObjectId",
                table: "AppUser",
                column: "ADObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_Id",
                table: "AppUser",
                column: "Id")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Model_BrandId",
                table: "Model",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Model_ProductCategoryId",
                table: "Model",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_AppUserId",
                table: "Offer",
                column: "AppUserId");

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
                name: "IX_Offer_SizeId",
                table: "Offer",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Size_ProductCategoryId",
                table: "Size",
                column: "ProductCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colour");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "ItemCondition");

            migrationBuilder.DropTable(
                name: "Model");

            migrationBuilder.DropTable(
                name: "OfferState");

            migrationBuilder.DropTable(
                name: "OfferType");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "ProductCategory");
        }
    }
}
