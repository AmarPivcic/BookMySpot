using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMySpotAPI.Migrations
{
    public partial class UpdateKlasa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_Administrator_KorisnickiNalog_OsobaID",
               table: "Administrator");

            migrationBuilder.DropForeignKey(
                name: "FK_Korisnik_KorisnickiNalog_OsobaID",
                table: "Korisnik");

            migrationBuilder.DropForeignKey(
                name: "FK_Manager_KorisnickiNalog_OsobaID",
                table: "Manager");

            migrationBuilder.RenameColumn(
                name: "Telefon",
                table: "UsluzniObjekt",
                newName: "telefon");

            migrationBuilder.RenameColumn(
                name: "Slika",
                table: "UsluzniObjekt",
                newName: "slika");

            migrationBuilder.RenameColumn(
                name: "NazivObjekta",
                table: "UsluzniObjekt",
                newName: "nazivObjekta");

            migrationBuilder.RenameColumn(
                name: "ManagerID",
                table: "UsluzniObjekt",
                newName: "managerID");

            migrationBuilder.RenameColumn(
                name: "Adresa",
                table: "UsluzniObjekt",
                newName: "adresa");

            migrationBuilder.RenameColumn(
                name: "UsluzniObjektID",
                table: "UsluzniObjekt",
                newName: "usluzniObjektID");

            migrationBuilder.RenameColumn(
                name: "UsluzniObjektID",
                table: "Usluga",
                newName: "usluzniObjektID");

            migrationBuilder.RenameColumn(
                name: "Trajanje",
                table: "Usluga",
                newName: "trajanje");

            migrationBuilder.RenameColumn(
                name: "Naziv",
                table: "Usluga",
                newName: "naziv");

            migrationBuilder.RenameColumn(
                name: "Cijena",
                table: "Usluga",
                newName: "cijena");

            migrationBuilder.RenameColumn(
                name: "UslugaID",
                table: "Usluga",
                newName: "uslugaID");

            migrationBuilder.RenameColumn(
                name: "UslugaID",
                table: "Rezervacija",
                newName: "uslugaID");

            migrationBuilder.RenameColumn(
                name: "KorisnikID",
                table: "Rezervacija",
                newName: "korisnikID");

            migrationBuilder.RenameColumn(
                name: "RezervacijaDatumVrijeme",
                table: "Rezervacija",
                newName: "datumRezervacije");

            migrationBuilder.RenameColumn(
                name: "OsobaID",
                table: "Manager",
                newName: "osobaID");

            migrationBuilder.RenameColumn(
                name: "OsobaID",
                table: "Korisnik",
                newName: "osobaID");

            migrationBuilder.RenameColumn(
                name: "Telefon",
                table: "KorisnickiNalog",
                newName: "telefon");

            migrationBuilder.RenameColumn(
                name: "Slika",
                table: "KorisnickiNalog",
                newName: "slika");

            migrationBuilder.RenameColumn(
                name: "Prezime",
                table: "KorisnickiNalog",
                newName: "prezime");

            migrationBuilder.RenameColumn(
                name: "Ime",
                table: "KorisnickiNalog",
                newName: "ime");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "KorisnickiNalog",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "OsobaID",
                table: "KorisnickiNalog",
                newName: "osobaID");

            migrationBuilder.RenameColumn(
                name: "OsobaID",
                table: "Administrator",
                newName: "osobaID");

            migrationBuilder.AlterColumn<string>(
                name: "slika",
                table: "UsluzniObjekt",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "prosjecnaOcjena",
                table: "UsluzniObjekt",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "rezervacijaKraj",
                table: "Rezervacija",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "rezervacijaPocetak",
                table: "Rezervacija",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "usluzniObjektID",
                table: "Rezervacija",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "slika",
                table: "KorisnickiNalog",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ManagerUsluzniObjekt",
                columns: table => new
                {
                    osobaID = table.Column<int>(type: "int", nullable: false),
                    usluzniObjektID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerUsluzniObjekt", x => new { x.osobaID, x.usluzniObjektID });
                    table.ForeignKey(
                        name: "FK_ManagerUsluzniObjekt_Manager_osobaID",
                        column: x => x.osobaID,
                        principalTable: "Manager",
                        principalColumn: "osobaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManagerUsluzniObjekt_UsluzniObjekt_usluzniObjektID",
                        column: x => x.usluzniObjektID,
                        principalTable: "UsluzniObjekt",
                        principalColumn: "usluzniObjektID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsluzniObjekt_KategorijaID",
                table: "UsluzniObjekt",
                column: "KategorijaID");

            migrationBuilder.CreateIndex(
                name: "IX_UsluzniObjekt_managerID",
                table: "UsluzniObjekt",
                column: "managerID");

            migrationBuilder.CreateIndex(
                name: "IX_Usluga_usluzniObjektID",
                table: "Usluga",
                column: "usluzniObjektID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_korisnikID",
                table: "Rezervacija",
                column: "korisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_uslugaID",
                table: "Rezervacija",
                column: "uslugaID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_usluzniObjektID",
                table: "Rezervacija",
                column: "usluzniObjektID");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerUsluzniObjekt_usluzniObjektID",
                table: "ManagerUsluzniObjekt",
                column: "usluzniObjektID");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrator_KorisnickiNalog_osobaID",
                table: "Administrator",
                column: "osobaID",
                principalTable: "KorisnickiNalog",
                principalColumn: "osobaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnik_KorisnickiNalog_osobaID",
                table: "Korisnik",
                column: "osobaID",
                principalTable: "KorisnickiNalog",
                principalColumn: "osobaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Manager_KorisnickiNalog_osobaID",
                table: "Manager",
                column: "osobaID",
                principalTable: "KorisnickiNalog",
                principalColumn: "osobaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Korisnik_korisnikID",
                table: "Rezervacija",
                column: "korisnikID",
                principalTable: "Korisnik",
                principalColumn: "osobaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Usluga_uslugaID",
                table: "Rezervacija",
                column: "uslugaID",
                principalTable: "Usluga",
                principalColumn: "uslugaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_UsluzniObjekt_usluzniObjektID",
                table: "Rezervacija",
                column: "usluzniObjektID",
                principalTable: "UsluzniObjekt",
                principalColumn: "usluzniObjektID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usluga_UsluzniObjekt_usluzniObjektID",
                table: "Usluga",
                column: "usluzniObjektID",
                principalTable: "UsluzniObjekt",
                principalColumn: "usluzniObjektID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsluzniObjekt_Kategorija_KategorijaID",
                table: "UsluzniObjekt",
                column: "KategorijaID",
                principalTable: "Kategorija",
                principalColumn: "KategorijaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsluzniObjekt_Manager_managerID",
                table: "UsluzniObjekt",
                column: "managerID",
                principalTable: "Manager",
                principalColumn: "osobaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropForeignKey(
                name: "FK_Usluga_UsluzniObjekt_usluzniObjektID",
                table: "Usluga");

            migrationBuilder.AddForeignKey(
                name: "FK_Usluga_UsluzniObjekt_usluzniObjektID",
                table: "Usluga",
                column: "usluzniObjektID",
                principalTable: "UsluzniObjekt",
                principalColumn: "usluzniObjektID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropForeignKey(
               name: "FK_Usluga_UsluzniObjekt_usluzniObjektID",
               table: "Usluga");

            migrationBuilder.AddForeignKey(
                name: "FK_Usluga_UsluzniObjekt_usluzniObjektID",
                table: "Usluga",
                column: "usluzniObjektID",
                principalTable: "UsluzniObjekt",
                principalColumn: "usluzniObjektID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrator_KorisnickiNalog_osobaID",
                table: "Administrator");

            migrationBuilder.DropForeignKey(
                name: "FK_Korisnik_KorisnickiNalog_osobaID",
                table: "Korisnik");

            migrationBuilder.DropForeignKey(
                name: "FK_Manager_KorisnickiNalog_osobaID",
                table: "Manager");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Korisnik_korisnikID",
                table: "Rezervacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Usluga_uslugaID",
                table: "Rezervacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_UsluzniObjekt_usluzniObjektID",
                table: "Rezervacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Usluga_UsluzniObjekt_usluzniObjektID",
                table: "Usluga");

            migrationBuilder.DropForeignKey(
                name: "FK_UsluzniObjekt_Kategorija_KategorijaID",
                table: "UsluzniObjekt");

            migrationBuilder.DropForeignKey(
                name: "FK_UsluzniObjekt_Manager_managerID",
                table: "UsluzniObjekt");

            migrationBuilder.DropTable(
                name: "ManagerUsluzniObjekt");

            migrationBuilder.DropIndex(
                name: "IX_UsluzniObjekt_KategorijaID",
                table: "UsluzniObjekt");

            migrationBuilder.DropIndex(
                name: "IX_UsluzniObjekt_managerID",
                table: "UsluzniObjekt");

            migrationBuilder.DropIndex(
                name: "IX_Usluga_usluzniObjektID",
                table: "Usluga");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacija_korisnikID",
                table: "Rezervacija");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacija_uslugaID",
                table: "Rezervacija");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacija_usluzniObjektID",
                table: "Rezervacija");

            migrationBuilder.DropColumn(
                name: "prosjecnaOcjena",
                table: "UsluzniObjekt");

            migrationBuilder.DropColumn(
                name: "rezervacijaKraj",
                table: "Rezervacija");

            migrationBuilder.DropColumn(
                name: "rezervacijaPocetak",
                table: "Rezervacija");

            migrationBuilder.DropColumn(
                name: "usluzniObjektID",
                table: "Rezervacija");

            migrationBuilder.RenameColumn(
                name: "telefon",
                table: "UsluzniObjekt",
                newName: "Telefon");

            migrationBuilder.RenameColumn(
                name: "slika",
                table: "UsluzniObjekt",
                newName: "Slika");

            migrationBuilder.RenameColumn(
                name: "nazivObjekta",
                table: "UsluzniObjekt",
                newName: "NazivObjekta");

            migrationBuilder.RenameColumn(
                name: "managerID",
                table: "UsluzniObjekt",
                newName: "ManagerID");

            migrationBuilder.RenameColumn(
                name: "adresa",
                table: "UsluzniObjekt",
                newName: "Adresa");

            migrationBuilder.RenameColumn(
                name: "usluzniObjektID",
                table: "UsluzniObjekt",
                newName: "UsluzniObjektID");

            migrationBuilder.RenameColumn(
                name: "usluzniObjektID",
                table: "Usluga",
                newName: "UsluzniObjektID");

            migrationBuilder.RenameColumn(
                name: "trajanje",
                table: "Usluga",
                newName: "Trajanje");

            migrationBuilder.RenameColumn(
                name: "naziv",
                table: "Usluga",
                newName: "Naziv");

            migrationBuilder.RenameColumn(
                name: "cijena",
                table: "Usluga",
                newName: "Cijena");

            migrationBuilder.RenameColumn(
                name: "uslugaID",
                table: "Usluga",
                newName: "UslugaID");

            migrationBuilder.RenameColumn(
                name: "uslugaID",
                table: "Rezervacija",
                newName: "UslugaID");

            migrationBuilder.RenameColumn(
                name: "korisnikID",
                table: "Rezervacija",
                newName: "KorisnikID");

            migrationBuilder.RenameColumn(
                name: "datumRezervacije",
                table: "Rezervacija",
                newName: "RezervacijaDatumVrijeme");

            migrationBuilder.RenameColumn(
                name: "osobaID",
                table: "Manager",
                newName: "OsobaID");

            migrationBuilder.RenameColumn(
                name: "osobaID",
                table: "Korisnik",
                newName: "OsobaID");

            migrationBuilder.RenameColumn(
                name: "telefon",
                table: "KorisnickiNalog",
                newName: "Telefon");

            migrationBuilder.RenameColumn(
                name: "slika",
                table: "KorisnickiNalog",
                newName: "Slika");

            migrationBuilder.RenameColumn(
                name: "prezime",
                table: "KorisnickiNalog",
                newName: "Prezime");

            migrationBuilder.RenameColumn(
                name: "ime",
                table: "KorisnickiNalog",
                newName: "Ime");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "KorisnickiNalog",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "osobaID",
                table: "KorisnickiNalog",
                newName: "OsobaID");

            migrationBuilder.RenameColumn(
                name: "osobaID",
                table: "Administrator",
                newName: "OsobaID");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Slika",
                table: "UsluzniObjekt",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Slika",
                table: "KorisnickiNalog",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Administrator_KorisnickiNalog_OsobaID",
                table: "Administrator",
                column: "OsobaID",
                principalTable: "KorisnickiNalog",
                principalColumn: "OsobaID");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Usluga_UsluzniObjekt_usluzniObjektID",
                table: "Usluga");

            migrationBuilder.AddForeignKey(
                name: "FK_Usluga_UsluzniObjekt_usluzniObjektID",
                table: "Usluga",
                column: "usluzniObjektID",
                principalTable: "UsluzniObjekt",
                principalColumn: "usluzniObjektID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_Usluga_UsluzniObjekt_usluzniObjektID",
                table: "Usluga");

            migrationBuilder.AddForeignKey(
                name: "FK_Usluga_UsluzniObjekt_usluzniObjektID",
                table: "Usluga",
                column: "usluzniObjektID",
                principalTable: "UsluzniObjekt",
                principalColumn: "usluzniObjektID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
