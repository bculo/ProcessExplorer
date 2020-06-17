using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessExplorer.Persistence.Migrations
{
    public partial class Authenticationtableadded : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authentication");
        }
    }
}
