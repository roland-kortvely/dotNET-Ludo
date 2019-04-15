using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoLibrary.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Comments",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Comments", x => x.Id); });

            migrationBuilder.CreateTable(
                "Ratings",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Stars = table.Column<int>()
                },
                constraints: table => { table.PrimaryKey("PK_Ratings", x => x.Id); });

            migrationBuilder.CreateTable(
                "Scores",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Points = table.Column<int>()
                },
                constraints: table => { table.PrimaryKey("PK_Scores", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Comments");

            migrationBuilder.DropTable(
                "Ratings");

            migrationBuilder.DropTable(
                "Scores");
        }
    }
}