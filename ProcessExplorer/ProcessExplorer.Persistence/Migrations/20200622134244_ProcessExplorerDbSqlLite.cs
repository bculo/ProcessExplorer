using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessExplorer.Persistence.Migrations
{
    public partial class ProcessExplorerDbSqlLite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authentication",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(maxLength: 1000, nullable: true),
                    Inserted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authentication", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    Started = table.Column<DateTime>(nullable: false),
                    OS = table.Column<string>(nullable: true),
                    Finished = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApplicationName = table.Column<string>(maxLength: 300, nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: false),
                    Saved = table.Column<DateTime>(nullable: false)
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
                    ProcessName = table.Column<string>(maxLength: 300, nullable: false),
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
                name: "Authentication");

            migrationBuilder.DropTable(
                name: "ProcessEntity");

            migrationBuilder.DropTable(
                name: "Session");
        }
    }
}
