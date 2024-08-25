﻿// <auto-generated />
using System;
using BookMySpotAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookMySpotAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240825194342_Suspenzije 3")]
    partial class Suspenzije3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BookMySpotAPI.Autentifikacija.Models.AutentifikacijaToken", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("OsobaID")
                        .HasColumnType("int");

                    b.Property<string>("ipAdresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("vrijednost")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("vrijemeEvidentiranja")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("OsobaID");

                    b.ToTable("AutentifikacijaToken");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.Grad", b =>
                {
                    b.Property<int>("gradID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("gradID"), 1L, 1);

                    b.Property<string>("naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("gradID");

                    b.ToTable("Gradovi");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.Kategorija", b =>
                {
                    b.Property<int>("kategorijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("kategorijaID"), 1L, 1);

                    b.Property<string>("naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("slika")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("kategorijaID");

                    b.ToTable("Kategorija");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.KorisnickiNalog", b =>
                {
                    b.Property<int>("osobaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("osobaID"), 1L, 1);

                    b.Property<DateTime?>("datumSuspenzijeDo")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("korisnickoIme")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lozinka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("obrisan")
                        .HasColumnType("bit");

                    b.Property<string>("prezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("razlogSuspenzije")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("slika")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("suspendovan")
                        .HasColumnType("bit");

                    b.Property<string>("telefon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("osobaID");

                    b.ToTable("KorisnickiNalog");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.KreditnaKartica", b =>
                {
                    b.Property<int>("karticaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("karticaID"), 1L, 1);

                    b.Property<string>("brojKartice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("datumIsteka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("korisnikID")
                        .HasColumnType("int");

                    b.Property<string>("sigurnosniBroj")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("karticaID");

                    b.HasIndex("korisnikID");

                    b.ToTable("KreditneKartice");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.ManagerUsluzniObjekt", b =>
                {
                    b.Property<int>("osobaID")
                        .HasColumnType("int");

                    b.Property<int>("usluzniObjektID")
                        .HasColumnType("int");

                    b.HasKey("osobaID", "usluzniObjektID");

                    b.HasIndex("usluzniObjektID");

                    b.ToTable("ManagerUsluzniObjekt");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.ONamaSadrzaj", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Tekst")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SadrzajiONama");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.PitanjeOdgovor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DatumKreiranja")
                        .HasColumnType("datetime2");

                    b.Property<int>("KorisnickiNalogId")
                        .HasColumnType("int");

                    b.Property<string>("Odgovor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pitanje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KorisnickiNalogId");

                    b.ToTable("PitanjaOdgovori");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.Recenzija", b =>
                {
                    b.Property<int>("recenzijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("recenzijaID"), 1L, 1);

                    b.Property<int>("KorisnickiNalogId")
                        .HasColumnType("int");

                    b.Property<int>("recenzijaOcjena")
                        .HasColumnType("int");

                    b.Property<string>("recenzijaTekst")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("usluzniObjektID")
                        .HasColumnType("int");

                    b.HasKey("recenzijaID");

                    b.HasIndex("KorisnickiNalogId");

                    b.HasIndex("usluzniObjektID");

                    b.ToTable("Recenzija");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.Rezervacija", b =>
                {
                    b.Property<int>("rezervacijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("rezervacijaID"), 1L, 1);

                    b.Property<DateTime>("datumRezervacije")
                        .HasColumnType("datetime2");

                    b.Property<bool>("karticnoPlacanje")
                        .HasColumnType("bit");

                    b.Property<int>("korisnikID")
                        .HasColumnType("int");

                    b.Property<bool>("otkazano")
                        .HasColumnType("bit");

                    b.Property<string>("rezervacijaKraj")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("rezervacijaPocetak")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("uslugaID")
                        .HasColumnType("int");

                    b.Property<int>("usluzniObjektID")
                        .HasColumnType("int");

                    b.Property<bool>("zavrseno")
                        .HasColumnType("bit");

                    b.HasKey("rezervacijaID");

                    b.HasIndex("korisnikID");

                    b.HasIndex("uslugaID");

                    b.HasIndex("usluzniObjektID");

                    b.ToTable("Rezervacija");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.Usluga", b =>
                {
                    b.Property<int>("uslugaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("uslugaID"), 1L, 1);

                    b.Property<string>("cijena")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("trajanje")
                        .HasColumnType("int");

                    b.Property<int>("usluzniObjektID")
                        .HasColumnType("int");

                    b.HasKey("uslugaID");

                    b.HasIndex("usluzniObjektID");

                    b.ToTable("Usluga");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.UsluzniObjekt", b =>
                {
                    b.Property<int>("usluzniObjektID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("usluzniObjektID"), 1L, 1);

                    b.Property<string>("adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("gradID")
                        .HasColumnType("int");

                    b.Property<bool>("isSmjestaj")
                        .HasColumnType("bit");

                    b.Property<int>("kategorijaID")
                        .HasColumnType("int");

                    b.Property<double?>("latitude")
                        .HasColumnType("float");

                    b.Property<double?>("longitude")
                        .HasColumnType("float");

                    b.Property<string>("nazivObjekta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("radnoVrijemeKraj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("radnoVrijemePocetak")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("slika")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("telefon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("usluzniObjektID");

                    b.HasIndex("gradID");

                    b.HasIndex("kategorijaID");

                    b.ToTable("UsluzniObjekt");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.Administrator", b =>
                {
                    b.HasBaseType("BookMySpotAPI.Modul.Models.KorisnickiNalog");

                    b.Property<string>("PIN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Administrator");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.Korisnik", b =>
                {
                    b.HasBaseType("BookMySpotAPI.Modul.Models.KorisnickiNalog");

                    b.Property<int>("brojRezervacija")
                        .HasColumnType("int");

                    b.ToTable("Korisnik");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.Manager", b =>
                {
                    b.HasBaseType("BookMySpotAPI.Modul.Models.KorisnickiNalog");

                    b.Property<string>("pozicija")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Manager");
                });

            modelBuilder.Entity("BookMySpotAPI.Autentifikacija.Models.AutentifikacijaToken", b =>
                {
                    b.HasOne("BookMySpotAPI.Modul.Models.KorisnickiNalog", "KorisnickiNalog")
                        .WithMany()
                        .HasForeignKey("OsobaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KorisnickiNalog");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.KreditnaKartica", b =>
                {
                    b.HasOne("BookMySpotAPI.Modul.Models.Korisnik", "korisnik")
                        .WithMany()
                        .HasForeignKey("korisnikID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("korisnik");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.ManagerUsluzniObjekt", b =>
                {
                    b.HasOne("BookMySpotAPI.Modul.Models.Manager", "manager")
                        .WithMany("managerUsluzniObjekt")
                        .HasForeignKey("osobaID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BookMySpotAPI.Modul.Models.UsluzniObjekt", "usluzniObjekt")
                        .WithMany("managerUsluzniObjekt")
                        .HasForeignKey("usluzniObjektID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("manager");

                    b.Navigation("usluzniObjekt");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.PitanjeOdgovor", b =>
                {
                    b.HasOne("BookMySpotAPI.Modul.Models.KorisnickiNalog", "korisnickiNalog")
                        .WithMany()
                        .HasForeignKey("KorisnickiNalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("korisnickiNalog");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.Recenzija", b =>
                {
                    b.HasOne("BookMySpotAPI.Modul.Models.KorisnickiNalog", "korisnickiNalog")
                        .WithMany()
                        .HasForeignKey("KorisnickiNalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookMySpotAPI.Modul.Models.UsluzniObjekt", "usluzniObjekt")
                        .WithMany()
                        .HasForeignKey("usluzniObjektID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("korisnickiNalog");

                    b.Navigation("usluzniObjekt");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.Rezervacija", b =>
                {
                    b.HasOne("BookMySpotAPI.Modul.Models.KorisnickiNalog", "korisnik")
                        .WithMany()
                        .HasForeignKey("korisnikID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookMySpotAPI.Modul.Models.Usluga", "usluga")
                        .WithMany()
                        .HasForeignKey("uslugaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookMySpotAPI.Modul.Models.UsluzniObjekt", "usluzniObjekt")
                        .WithMany()
                        .HasForeignKey("usluzniObjektID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("korisnik");

                    b.Navigation("usluga");

                    b.Navigation("usluzniObjekt");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.Usluga", b =>
                {
                    b.HasOne("BookMySpotAPI.Modul.Models.UsluzniObjekt", "usluzniObjekt")
                        .WithMany()
                        .HasForeignKey("usluzniObjektID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("usluzniObjekt");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.UsluzniObjekt", b =>
                {
                    b.HasOne("BookMySpotAPI.Modul.Models.Grad", "grad")
                        .WithMany()
                        .HasForeignKey("gradID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BookMySpotAPI.Modul.Models.Kategorija", "kategorija")
                        .WithMany()
                        .HasForeignKey("kategorijaID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("grad");

                    b.Navigation("kategorija");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.Administrator", b =>
                {
                    b.HasOne("BookMySpotAPI.Modul.Models.KorisnickiNalog", null)
                        .WithOne()
                        .HasForeignKey("BookMySpotAPI.Modul.Models.Administrator", "osobaID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.Korisnik", b =>
                {
                    b.HasOne("BookMySpotAPI.Modul.Models.KorisnickiNalog", null)
                        .WithOne()
                        .HasForeignKey("BookMySpotAPI.Modul.Models.Korisnik", "osobaID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.Manager", b =>
                {
                    b.HasOne("BookMySpotAPI.Modul.Models.KorisnickiNalog", null)
                        .WithOne()
                        .HasForeignKey("BookMySpotAPI.Modul.Models.Manager", "osobaID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.UsluzniObjekt", b =>
                {
                    b.Navigation("managerUsluzniObjekt");
                });

            modelBuilder.Entity("BookMySpotAPI.Modul.Models.Manager", b =>
                {
                    b.Navigation("managerUsluzniObjekt");
                });
#pragma warning restore 612, 618
        }
    }
}
