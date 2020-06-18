using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessExplorerWeb.Infrastructure.Migrations
{
    public partial class IdentityUserTableModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                schema: "Security",
                table: "IdentityAppUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                schema: "Security",
                table: "IdentityAppUser",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
