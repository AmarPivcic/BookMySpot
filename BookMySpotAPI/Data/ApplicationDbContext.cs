using Microsoft.EntityFrameworkCore;
using BookMySpotAPI.Modul.Models;

namespace BookMySpotAPI.Data
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<Osoba> Osobe { get; set; }
        public DbSet<UsluzniObjekt> UsluzniObjekti { get; set; }
        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<Administrator> Administratori { get; set; }
        public DbSet<Manager> Manageri { get; set; }
        public DbSet<Kategorija> Kategorije { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<KreditnaKartica> KreditneKartice { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
        public DbSet<Usluga> Usluge { get; set; }

        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }
    }
}