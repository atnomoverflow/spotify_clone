﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotify_clone2.Migrations
{
    public partial class songmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "songPath",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "songPath",
                table: "Songs");
        }
    }
}
