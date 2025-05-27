using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniqueShit.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPrivateChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessage_Chat_ChatId",
                table: "ChatMessage");

            migrationBuilder.DropTable(
                name: "ChatUser");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "ChatMessage",
                newName: "PrivateChatId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessage_ChatId",
                table: "ChatMessage",
                newName: "IX_ChatMessage_PrivateChatId");

            migrationBuilder.CreateTable(
                name: "PrivateChat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateChat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateChat_AppUser_User1Id",
                        column: x => x.User1Id,
                        principalTable: "AppUser",
                        principalColumn: "ADObjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrivateChat_AppUser_User2Id",
                        column: x => x.User2Id,
                        principalTable: "AppUser",
                        principalColumn: "ADObjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrivateChat_User1Id",
                table: "PrivateChat",
                column: "User1Id");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateChat_User2Id",
                table: "PrivateChat",
                column: "User2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessage_PrivateChat_PrivateChatId",
                table: "ChatMessage",
                column: "PrivateChatId",
                principalTable: "PrivateChat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessage_PrivateChat_PrivateChatId",
                table: "ChatMessage");

            migrationBuilder.DropTable(
                name: "PrivateChat");

            migrationBuilder.RenameColumn(
                name: "PrivateChatId",
                table: "ChatMessage",
                newName: "ChatId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessage_PrivateChatId",
                table: "ChatMessage",
                newName: "IX_ChatMessage_ChatId");

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatUser",
                columns: table => new
                {
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUser", x => new { x.ChatId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ChatUser_AppUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUser",
                        principalColumn: "ADObjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatUser_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatUser_UserId",
                table: "ChatUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessage_Chat_ChatId",
                table: "ChatMessage",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
