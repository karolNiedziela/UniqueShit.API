using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniqueShit.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAppUserIdToModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "Model",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Model_AppUserId",
                table: "Model",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Model_AppUser_AppUserId",
                table: "Model",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "ADObjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Model_AppUser_AppUserId",
                table: "Model");

            migrationBuilder.DropIndex(
                name: "IX_Model_AppUserId",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Model");
        }
    }
}
