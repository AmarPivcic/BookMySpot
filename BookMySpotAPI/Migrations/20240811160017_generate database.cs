﻿using System;
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
                    lozinka = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnickiNalog", x => x.osobaID);
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
                name: "UsluzniObjekt",
                columns: table => new
                {
                    usluzniObjektID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazivObjekta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    osobaID = table.Column<int>(type: "int", nullable: false),
                    managerosobaID = table.Column<int>(type: "int", nullable: false),
                    slika = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    kategorijaID = table.Column<int>(type: "int", nullable: false),
                    prosjecnaOcjena = table.Column<float>(type: "real", nullable: false),
                    gradID = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_UsluzniObjekt_Manager_managerosobaID",
                        column: x => x.managerosobaID,
                        principalTable: "Manager",
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
                name: "Usluga",
                columns: table => new
                {
                    uslugaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trajanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cijena = table.Column<float>(type: "real", nullable: false),
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
                name: "Rezervacija",
                columns: table => new
                {
                    rezervacijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datumRezervacije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    rezervacijaPocetak = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rezervacijaKraj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    korisnikID = table.Column<int>(type: "int", nullable: false),
                    uslugaID = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_Rezervacija_Usluga_uslugaID",
                        column: x => x.uslugaID,
                        principalTable: "Usluga",
                        principalColumn: "uslugaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutentifikacijaToken_OsobaID",
                table: "AutentifikacijaToken",
                column: "OsobaID");

            migrationBuilder.CreateIndex(
                name: "IX_KreditneKartice_korisnikID",
                table: "KreditneKartice",
                column: "korisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerUsluzniObjekt_usluzniObjektID",
                table: "ManagerUsluzniObjekt",
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

            migrationBuilder.CreateIndex(
                name: "IX_UsluzniObjekt_managerosobaID",
                table: "UsluzniObjekt",
                column: "managerosobaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "AutentifikacijaToken");

            migrationBuilder.DropTable(
                name: "KreditneKartice");

            migrationBuilder.DropTable(
                name: "ManagerUsluzniObjekt");

            migrationBuilder.DropTable(
                name: "Rezervacija");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Usluga");

            migrationBuilder.DropTable(
                name: "UsluzniObjekt");

            migrationBuilder.DropTable(
                name: "Gradovi");

            migrationBuilder.DropTable(
                name: "Kategorija");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "KorisnickiNalog");
        }
    }
}
