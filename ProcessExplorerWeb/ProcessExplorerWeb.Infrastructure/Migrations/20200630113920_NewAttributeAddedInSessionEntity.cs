using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessExplorerWeb.Infrastructure.Migrations
{
    public partial class NewAttributeAddedInSessionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ComputerSessionId",
                table: "ProcessExplorerUserSession",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComputerSessionId",
                table: "ProcessExplorerUserSession");
        }
    }
}
