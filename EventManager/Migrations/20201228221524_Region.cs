using Microsoft.EntityFrameworkCore.Migrations;

namespace EventManager.Migrations
{
    public partial class Region : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KeyWords",
                table: "Event",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Event",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyWords",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Event");
        }
    }
}
