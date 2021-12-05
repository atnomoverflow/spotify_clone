using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotify_clone2.Migrations
{
    public partial class DataOfCreatingForSong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "Songs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "Songs");
        }
    }
}
