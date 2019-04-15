using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoLibrary.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "Content",
                "Ratings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Content",
                "Ratings");
        }
    }
}