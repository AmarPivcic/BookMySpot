using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMySpotAPI.Migrations
{
    public partial class Brisanjeisuspendovanje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "obrisan",
                table: "KorisnickiNalog",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "suspendovan",
                table: "KorisnickiNalog",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "obrisan",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "suspendovan",
                table: "KorisnickiNalog");
        }
    }
}
