using Microsoft.EntityFrameworkCore;
using BookMySpotAPI.Modul.Models;
using BookMySpotAPI.Autentifikacija.Models;

namespace BookMySpotAPI.Data
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<KorisnickiNalog> KorisnickiNalog { get; set; }
        public DbSet<UsluzniObjekt> UsluzniObjekti { get; set; }
        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<Administrator> Administratori { get; set; }
        public DbSet<Manager> Manageri { get; set; }
        public DbSet<Kategorija> Kategorije { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<KreditnaKartica> KreditneKartice { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
        public DbSet<Usluga> Usluge { get; set; }
        public DbSet<AutentifikacijaToken> AutentifikacijaTokeni { get; set; }

        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }
    }
}