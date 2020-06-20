using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessExplorerWeb.Infrastructure.Migrations
{
    public partial class UserSessioNTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessExplorerUserSession",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 300, nullable: false),
                    Started = table.Column<DateTime>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    ExplorerUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessExplorerUserSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessExplorerUserSession_ProcessExplorerUser_ExplorerUserId",
                        column: x => x.ExplorerUserId,
                        principalTable: "ProcessExplorerUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessExplorerUserSession_ExplorerUserId",
                table: "ProcessExplorerUserSession",
                column: "ExplorerUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessExplorerUserSession");
        }
    }
}
