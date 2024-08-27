using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMySpotAPI.Migrations
{
    public partial class generatedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    gradID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.gradID);
                });

            migrationBuilder.CreateTable(
                name: "Kategorija",
                columns: table => new
                {
                    kategorijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    slika = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorija", x => x.kategorijaID);
                });

            migrationBuilder.CreateTable(
                name: "KorisnickiNalog",
                columns: table => new
                {
                    osobaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    korisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    slika = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lozinka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    obrisan = table.Column<bool>(type: "bit", nullable: false),
                    suspendovan = table.Column<bool>(type: "bit", nullable: false),
                    datumSuspenzijeDo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    razlogSuspenzije = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnickiNalog", x => x.osobaID);
                });

            migrationBuilder.CreateTable(
                name: "SadrzajiONama",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tekst = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SadrzajiONama", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsluzniObjekt",
                columns: table => new
                {
                    usluzniObjektID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazivObjekta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    radnoVrijemePocetak = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    radnoVrijemeKraj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    slika = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    kategorijaID = table.Column<int>(type: "int", nullable: false),
                    gradID = table.Column<int>(type: "int", nullable: false),
                    isSmjestaj = table.Column<bool>(type: "bit", nullable: false),
                    latitude = table.Column<double>(type: "float", nullable: true),
                    longitude = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsluzniObjekt", x => x.usluzniObjektID);
                    table.ForeignKey(
                        name: "FK_UsluzniObjekt_Gradovi_gradID",
                        column: x => x.gradID,
                        principalTable: "Gradovi",
                        principalColumn: "gradID");
                    table.ForeignKey(
                        name: "FK_UsluzniObjekt_Kategorija_kategorijaID",
                        column: x => x.kategorijaID,
                        principalTable: "Kategorija",
                        principalColumn: "kategorijaID");
                });

            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    osobaID = table.Column<int>(type: "int", nullable: false),
                    PIN = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.osobaID);
                    table.ForeignKey(
                        name: "FK_Administrator_KorisnickiNalog_osobaID",
                        column: x => x.osobaID,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "osobaID");
                });

            migrationBuilder.CreateTable(
                name: "AutentifikacijaToken",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vrijednost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OsobaID = table.Column<int>(type: "int", nullable: false),
                    vrijemeEvidentiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ipAdresa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutentifikacijaToken", x => x.id);
                    table.ForeignKey(
                        name: "FK_AutentifikacijaToken_KorisnickiNalog_OsobaID",
                        column: x => x.OsobaID,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "osobaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    osobaID = table.Column<int>(type: "int", nullable: false),
                    brojRezervacija = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.osobaID);
                    table.ForeignKey(
                        name: "FK_Korisnik_KorisnickiNalog_osobaID",
                        column: x => x.osobaID,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "osobaID");
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    osobaID = table.Column<int>(type: "int", nullable: false),
                    pozicija = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.osobaID);
                    table.ForeignKey(
                        name: "FK_Manager_KorisnickiNalog_osobaID",
                        column: x => x.osobaID,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "osobaID");
                });

            migrationBuilder.CreateTable(
                name: "PitanjaOdgovori",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickiNalogId = table.Column<int>(type: "int", nullable: false),
                    Pitanje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Odgovor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PitanjaOdgovori", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PitanjaOdgovori_KorisnickiNalog_KorisnickiNalogId",
                        column: x => x.KorisnickiNalogId,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "osobaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorit",
                columns: table => new
                {
                    favoritID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickiNalogId = table.Column<int>(type: "int", nullable: false),
                    usluzniObjektID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorit", x => x.favoritID);
                    table.ForeignKey(
                        name: "FK_Favorit_KorisnickiNalog_KorisnickiNalogId",
                        column: x => x.KorisnickiNalogId,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "osobaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorit_UsluzniObjekt_usluzniObjektID",
                        column: x => x.usluzniObjektID,
                        principalTable: "UsluzniObjekt",
                        principalColumn: "usluzniObjektID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recenzija",
                columns: table => new
                {
                    recenzijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recenzijaOcjena = table.Column<int>(type: "int", nullable: false),
                    recenzijaTekst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    usluzniObjektID = table.Column<int>(type: "int", nullable: false),
                    KorisnickiNalogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzija", x => x.recenzijaID);
                    table.ForeignKey(
                        name: "FK_Recenzija_KorisnickiNalog_KorisnickiNalogId",
                        column: x => x.KorisnickiNalogId,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "osobaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recenzija_UsluzniObjekt_usluzniObjektID",
                        column: x => x.usluzniObjektID,
                        principalTable: "UsluzniObjekt",
                        principalColumn: "usluzniObjektID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usluga",
                columns: table => new
                {
                    uslugaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trajanje = table.Column<int>(type: "int", nullable: true),
                    cijena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usluzniObjektID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usluga", x => x.uslugaID);
                    table.ForeignKey(
                        name: "FK_Usluga_UsluzniObjekt_usluzniObjektID",
                        column: x => x.usluzniObjektID,
                        principalTable: "UsluzniObjekt",
                        principalColumn: "usluzniObjektID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KreditneKartice",
                columns: table => new
                {
                    karticaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brojKartice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    datumIsteka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sigurnosniBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    korisnikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KreditneKartice", x => x.karticaID);
                    table.ForeignKey(
                        name: "FK_KreditneKartice_Korisnik_korisnikID",
                        column: x => x.korisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "osobaID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        principalColumn: "osobaID");
                    table.ForeignKey(
                        name: "FK_ManagerUsluzniObjekt_UsluzniObjekt_usluzniObjektID",
                        column: x => x.usluzniObjektID,
                        principalTable: "UsluzniObjekt",
                        principalColumn: "usluzniObjektID");
                });

            migrationBuilder.CreateTable(
                name: "Rezervacija",
                columns: table => new
                {
                    rezervacijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datumRezervacije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    rezervacijaPocetak = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rezervacijaKraj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    otkazano = table.Column<bool>(type: "bit", nullable: false),
                    zavrseno = table.Column<bool>(type: "bit", nullable: false),
                    korisnikID = table.Column<int>(type: "int", nullable: false),
                    uslugaID = table.Column<int>(type: "int", nullable: false),
                    usluzniObjektID = table.Column<int>(type: "int", nullable: false),
                    managerID = table.Column<int>(type: "int", nullable: false),
                    karticnoPlacanje = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacija", x => x.rezervacijaID);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Korisnik_korisnikID",
                        column: x => x.korisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "osobaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Manager_managerID",
                        column: x => x.managerID,
                        principalTable: "Manager",
                        principalColumn: "osobaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Usluga_uslugaID",
                        column: x => x.uslugaID,
                        principalTable: "Usluga",
                        principalColumn: "uslugaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervacija_UsluzniObjekt_usluzniObjektID",
                        column: x => x.usluzniObjektID,
                        principalTable: "UsluzniObjekt",
                        principalColumn: "usluzniObjektID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutentifikacijaToken_OsobaID",
                table: "AutentifikacijaToken",
                column: "OsobaID");

            migrationBuilder.CreateIndex(
                name: "IX_Favorit_KorisnickiNalogId",
                table: "Favorit",
                column: "KorisnickiNalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorit_usluzniObjektID",
                table: "Favorit",
                column: "usluzniObjektID");

            migrationBuilder.CreateIndex(
                name: "IX_KreditneKartice_korisnikID",
                table: "KreditneKartice",
                column: "korisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerUsluzniObjekt_usluzniObjektID",
                table: "ManagerUsluzniObjekt",
                column: "usluzniObjektID");

            migrationBuilder.CreateIndex(
                name: "IX_PitanjaOdgovori_KorisnickiNalogId",
                table: "PitanjaOdgovori",
                column: "KorisnickiNalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzija_KorisnickiNalogId",
                table: "Recenzija",
                column: "KorisnickiNalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzija_usluzniObjektID",
                table: "Recenzija",
                column: "usluzniObjektID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_korisnikID",
                table: "Rezervacija",
                column: "korisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_managerID",
                table: "Rezervacija",
                column: "managerID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_uslugaID",
                table: "Rezervacija",
                column: "uslugaID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_usluzniObjektID",
                table: "Rezervacija",
                column: "usluzniObjektID");

            migrationBuilder.CreateIndex(
                name: "IX_Usluga_usluzniObjektID",
                table: "Usluga",
                column: "usluzniObjektID");

            migrationBuilder.CreateIndex(
                name: "IX_UsluzniObjekt_gradID",
                table: "UsluzniObjekt",
                column: "gradID");

            migrationBuilder.CreateIndex(
                name: "IX_UsluzniObjekt_kategorijaID",
                table: "UsluzniObjekt",
                column: "kategorijaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "AutentifikacijaToken");

            migrationBuilder.DropTable(
                name: "Favorit");

            migrationBuilder.DropTable(
                name: "KreditneKartice");

            migrationBuilder.DropTable(
                name: "ManagerUsluzniObjekt");

            migrationBuilder.DropTable(
                name: "PitanjaOdgovori");

            migrationBuilder.DropTable(
                name: "Recenzija");

            migrationBuilder.DropTable(
                name: "Rezervacija");

            migrationBuilder.DropTable(
                name: "SadrzajiONama");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "Usluga");

            migrationBuilder.DropTable(
                name: "KorisnickiNalog");

            migrationBuilder.DropTable(
                name: "UsluzniObjekt");

            migrationBuilder.DropTable(
                name: "Gradovi");

            migrationBuilder.DropTable(
                name: "Kategorija");
        }
    }
}
