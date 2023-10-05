using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDesk.UI.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "content",
                table: "incidents",
                newName: "description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "incidents",
                newName: "content");
        }
    }
}
