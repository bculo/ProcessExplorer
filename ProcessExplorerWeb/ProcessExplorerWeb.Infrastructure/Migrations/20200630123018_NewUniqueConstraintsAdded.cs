using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessExplorerWeb.Infrastructure.Migrations
{
    public partial class NewUniqueConstraintsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProcessExplorerUserSession_ComputerSessionId_ExplorerUserId",
                table: "ProcessExplorerUserSession",
                columns: new[] { "ComputerSessionId", "ExplorerUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessEntity_ProcessName_SessionId",
                table: "ProcessEntity",
                columns: new[] { "ProcessName", "SessionId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProcessExplorerUserSession_ComputerSessionId_ExplorerUserId",
                table: "ProcessExplorerUserSession");

            migrationBuilder.DropIndex(
                name: "IX_ProcessEntity_ProcessName_SessionId",
                table: "ProcessEntity");
        }
    }
}
