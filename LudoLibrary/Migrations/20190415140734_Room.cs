using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoLibrary.Migrations
{
    public partial class Room : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "RoomId",
                "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                "Rooms",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Rooms", x => x.Id); });

            migrationBuilder.CreateIndex(
                "IX_Users_RoomId",
                "Users",
                "RoomId");

            migrationBuilder.AddForeignKey(
                "FK_Users_Rooms_RoomId",
                "Users",
                "RoomId",
                "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Users_Rooms_RoomId",
                "Users");

            migrationBuilder.DropTable(
                "Rooms");

            migrationBuilder.DropIndex(
                "IX_Users_RoomId",
                "Users");

            migrationBuilder.DropColumn(
                "RoomId",
                "Users");
        }
    }
}