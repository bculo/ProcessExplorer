using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessExplorer.Persistence.Migrations
{
    public partial class ProccesTableAndApplicationTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Session",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ApplicationEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApplicationName = table.Column<string>(maxLength: 300, nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    Saved = table.Column<DateTime>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationEntity_Session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcessEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcessId = table.Column<int>(nullable: false),
                    ProcessName = table.Column<int>(maxLength: 300, nullable: false),
                    Saved = table.Column<DateTime>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessEntity_Session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Session",
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
                name: "UserName",
                table: "Session");
        }
    }
}
