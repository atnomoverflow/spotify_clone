using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotify_clone2.Migrations
{
    public partial class moreDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArtisteId",
                table: "Songs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bio",
                table: "Artistes",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ArtisteId",
                table: "Songs",
                column: "ArtisteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Artistes_ArtisteId",
                table: "Songs",
                column: "ArtisteId",
                principalTable: "Artistes",
                principalColumn: "ArtisteId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Artistes_ArtisteId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_ArtisteId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "ArtisteId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "bio",
                table: "Artistes");
        }
    }
}
