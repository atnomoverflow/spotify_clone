using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotify_clone2.Migrations
{
    public partial class JupiterMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_AspNetUsers_userID",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayLists_Client_ClientId",
                table: "PlayLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client",
                table: "Client");

            migrationBuilder.RenameTable(
                name: "Client",
                newName: "Clients");

            migrationBuilder.RenameIndex(
                name: "IX_Client_userID",
                table: "Clients",
                newName: "IX_Clients_userID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_AspNetUsers_userID",
                table: "Clients",
                column: "userID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayLists_Clients_ClientId",
                table: "PlayLists",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_AspNetUsers_userID",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayLists_Clients_ClientId",
                table: "PlayLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Client");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_userID",
                table: "Client",
                newName: "IX_Client_userID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client",
                table: "Client",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_AspNetUsers_userID",
                table: "Client",
                column: "userID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayLists_Client_ClientId",
                table: "PlayLists",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
