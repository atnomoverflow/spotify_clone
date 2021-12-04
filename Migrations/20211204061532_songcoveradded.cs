using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotify_clone2.Migrations
{
    public partial class songcoveradded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "duration",
                table: "Songs");

            migrationBuilder.AddColumn<string>(
                name: "songCover",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "songCover",
                table: "Songs");

            migrationBuilder.AddColumn<double>(
                name: "duration",
                table: "Songs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
