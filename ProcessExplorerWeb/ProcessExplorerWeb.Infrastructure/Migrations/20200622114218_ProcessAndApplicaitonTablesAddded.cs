using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessExplorerWeb.Infrastructure.Migrations
{
    public partial class ProcessAndApplicaitonTablesAddded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OS",
                table: "ProcessExplorerUserSession",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    Started = table.Column<DateTime>(nullable: false),
                    Closed = table.Column<DateTime>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationEntity_ProcessExplorerUserSession_SessionId",
                        column: x => x.SessionId,
                        principalTable: "ProcessExplorerUserSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcessEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProcessName = table.Column<string>(maxLength: 300, nullable: false),
                    PID = table.Column<int>(nullable: true),
                    SessionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessEntity_ProcessExplorerUserSession_SessionId",
                        column: x => x.SessionId,
                        principalTable: "ProcessExplorerUserSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationEntity_SessionId",
                table: "ApplicationEntity",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessEntity_SessionId",
                table: "ProcessEntity",
                column: "SessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationEntity");

            migrationBuilder.DropTable(
                name: "ProcessEntity");

            migrationBuilder.DropColumn(
                name: "OS",
                table: "ProcessExplorerUserSession");
        }
    }
}
