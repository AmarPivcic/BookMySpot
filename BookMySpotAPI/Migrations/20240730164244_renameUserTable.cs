using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMySpotAPI.Migrations
{
    public partial class renameUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrator_Osoba_OsobaID",
                table: "Administrator");

            migrationBuilder.DropForeignKey(
                name: "FK_AutentifikacijaToken_Osoba_OsobaID",
                table: "AutentifikacijaToken");

            migrationBuilder.DropForeignKey(
                name: "FK_Korisnik_Osoba_OsobaID",
                table: "Korisnik");

            migrationBuilder.DropForeignKey(
                name: "FK_Manager_Osoba_OsobaID",
                table: "Manager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Osoba",
                table: "Osoba");

            migrationBuilder.RenameTable(
                name: "Osoba",
                newName: "KorisnickiNalog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KorisnickiNalog",
                table: "KorisnickiNalog",
                column: "OsobaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrator_KorisnickiNalog_OsobaID",
                table: "Administrator",
                column: "OsobaID",
                principalTable: "KorisnickiNalog",
                principalColumn: "OsobaID");

            migrationBuilder.AddForeignKey(
                name: "FK_AutentifikacijaToken_KorisnickiNalog_OsobaID",
                table: "AutentifikacijaToken",
                column: "OsobaID",
                principalTable: "KorisnickiNalog",
                principalColumn: "OsobaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnik_KorisnickiNalog_OsobaID",
                table: "Korisnik",
                column: "OsobaID",
                principalTable: "KorisnickiNalog",
                principalColumn: "OsobaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Manager_KorisnickiNalog_OsobaID",
                table: "Manager",
                column: "OsobaID",
                principalTable: "KorisnickiNalog",
                principalColumn: "OsobaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrator_KorisnickiNalog_OsobaID",
                table: "Administrator");

            migrationBuilder.DropForeignKey(
                name: "FK_AutentifikacijaToken_KorisnickiNalog_OsobaID",
                table: "AutentifikacijaToken");

            migrationBuilder.DropForeignKey(
                name: "FK_Korisnik_KorisnickiNalog_OsobaID",
                table: "Korisnik");

            migrationBuilder.DropForeignKey(
                name: "FK_Manager_KorisnickiNalog_OsobaID",
                table: "Manager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KorisnickiNalog",
                table: "KorisnickiNalog");

            migrationBuilder.RenameTable(
                name: "KorisnickiNalog",
                newName: "Osoba");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Osoba",
                table: "Osoba",
                column: "OsobaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrator_Osoba_OsobaID",
                table: "Administrator",
                column: "OsobaID",
                principalTable: "Osoba",
                principalColumn: "OsobaID");

            migrationBuilder.AddForeignKey(
                name: "FK_AutentifikacijaToken_Osoba_OsobaID",
                table: "AutentifikacijaToken",
                column: "OsobaID",
                principalTable: "Osoba",
                principalColumn: "OsobaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnik_Osoba_OsobaID",
                table: "Korisnik",
                column: "OsobaID",
                principalTable: "Osoba",
                principalColumn: "OsobaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Manager_Osoba_OsobaID",
                table: "Manager",
                column: "OsobaID",
                principalTable: "Osoba",
                principalColumn: "OsobaID");
        }
    }
}
