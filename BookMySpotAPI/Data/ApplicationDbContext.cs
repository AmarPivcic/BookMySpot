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
        public DbSet<ManagerUsluzniObjekt> ManagerUsluzniObjekti { get; set; }
        public DbSet<ONamaSadrzaj> SadrzajiONama { get; set; }

        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // postavljanje kompozitnog primarnog ključa za tabelu ManagerUsluzniObjekt
            modelBuilder.Entity<ManagerUsluzniObjekt>()
                .HasKey(mb => new { mb.osobaID, mb.usluzniObjektID });

            // podešavanje many to many relationshipa
            modelBuilder.Entity<ManagerUsluzniObjekt>()
                .HasOne(mb => mb.manager)
                .WithMany(m => m.managerUsluzniObjekt)
                .HasForeignKey(mb => mb.osobaID)
                .OnDelete(DeleteBehavior.NoAction);  //rješavanje problema sa kaskadnim brisanjem (kada se briše manager)

            modelBuilder.Entity<ManagerUsluzniObjekt>()
                .HasOne(mb => mb.usluzniObjekt)
                .WithMany(b => b.managerUsluzniObjekt)
                .HasForeignKey(mb => mb.usluzniObjektID)
                .OnDelete(DeleteBehavior.NoAction); //rješavanje problema sa kaskadnim brisanjem (kada se briše uslužni objekt)

            modelBuilder.Entity<UsluzniObjekt>()
                .HasOne(u => u.grad)
                .WithMany()
                .HasForeignKey(u => u.gradID)
                .OnDelete(DeleteBehavior.NoAction);  //rješavanje problema sa kaskadnim brisanjem (kada se briše grad)

            modelBuilder.Entity<UsluzniObjekt>()  //rješavanje problema sa kaskadnim brisanjem (kada se briše kategorija)
                .HasOne(u => u.kategorija)
                .WithMany()
                .HasForeignKey(u => u.kategorijaID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rezervacija>()  //rješavanje problema sa kaskadnim brisanjem (kada se briše rezervacija)
                .HasOne(r => r.usluzniObjekt)
                .WithMany()
                .HasForeignKey(r => r.usluzniObjektID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}