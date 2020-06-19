using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessExplorerWeb.Infrastructure.Migrations
{
    public partial class ProcessExplorerUserTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessExplorerUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessExplorerUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessExplorerUser_Email",
                table: "ProcessExplorerUser",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessExplorerUser_UserName",
                table: "ProcessExplorerUser",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessExplorerUser");
        }
    }
}
