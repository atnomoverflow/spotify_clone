using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotify_clone2.Migrations
{
    public partial class changingMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Memeberships",
                table: "Memeberships");

            migrationBuilder.DropColumn(
                name: "MemebershipId",
                table: "Memeberships");

            migrationBuilder.DropColumn(
                name: "MembershipType",
                table: "Memeberships");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "Memeberships",
                newName: "CurrentPeriodEnd");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Memeberships",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Memeberships",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Memeberships",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Memeberships",
                table: "Memeberships",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Memeberships",
                table: "Memeberships");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Memeberships");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Memeberships");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Memeberships");

            migrationBuilder.RenameColumn(
                name: "CurrentPeriodEnd",
                table: "Memeberships",
                newName: "ExpirationDate");

            migrationBuilder.AddColumn<int>(
                name: "MemebershipId",
                table: "Memeberships",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "MembershipType",
                table: "Memeberships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Memeberships",
                table: "Memeberships",
                column: "MemebershipId");
        }
    }
}
