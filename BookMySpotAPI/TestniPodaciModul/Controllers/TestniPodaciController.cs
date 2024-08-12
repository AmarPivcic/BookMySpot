using BookMySpotAPI.Data;
using BookMySpotAPI.Helper;
using BookMySpotAPI.Modul.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMySpotAPI.TestniPodaciModul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestniPodaciController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly PasswordHasher _passwordHasher;

        public TestniPodaciController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _passwordHasher = new PasswordHasher();
        }

        [HttpPost]
        public async Task <ActionResult> Generate()
        {
            var korisnik = new Korisnik
            {
                ime = "UserIme",
                prezime = "UserPrezime",
                email = "user@gmail.com",
                telefon = "+38763222222",
                korisnickoIme = "user",
                brojRezervacija = 2
            };

            var hashedPassword = _passwordHasher.HashPassword(korisnik, "user");
            korisnik.lozinka = hashedPassword;

            await _dbContext.Korisnici.AddAsync(korisnik);
            await _dbContext.SaveChangesAsync();


            var smjestaj = new Kategorija
            {
                naziv = "Smještaj",
                slika = "https://localhost:7058/Slike/room.jpg"
            };

            await _dbContext.Kategorije.AddAsync(smjestaj);
            await _dbContext.SaveChangesAsync();


            var saloniLjepote = new Kategorija
            {
                naziv = "Saloni Ljepote",
                slika = "https://localhost:7058/Slike/beauty.jpg"
            };

            await _dbContext.Kategorije.AddAsync(saloniLjepote);
            await _dbContext.SaveChangesAsync();


            var ugostiteljstvo = new Kategorija
            {
                naziv = "Ugostiteljstvo",
                slika = "https://localhost:7058/Slike/restoran.jpg"
            };

            await _dbContext.Kategorije.AddAsync(ugostiteljstvo);
            await _dbContext.SaveChangesAsync();


            var ordinacije = new Kategorija
            {
                naziv = "Ordinacije",
                slika = "https://localhost:7058/Slike/ordinacija.jpg"
            };

            await _dbContext.Kategorije.AddAsync(ordinacije);
            await _dbContext.SaveChangesAsync();


            var vozila = new Kategorija
            {
                naziv = "Vozila",
                slika = "https://localhost:7058/Slike/vozila.jpg"
            };

            await _dbContext.Kategorije.AddAsync(vozila);
            await _dbContext.SaveChangesAsync();


            var oNamaSadrzaj = new ONamaSadrzaj
            {
                Tekst = "<center>\n\n<img src=\"/assets/BookMySpot.png\" alt=\"BookMySpot\" width=\"150\" height=\"100\" />\n\n### Dobrodošli u BookMySpot!\n\n<br>\n\nBookMySpot je inovativna platforma koja vam omogućava jednostavno i\n        brzo rezerviranje smještaja, salona ljepote, ugostiteljskih objekata,\n        ordinacija i raznih usluga, sve na jednom mjestu. Naša misija je\n        spojiti vas s najboljim destinacijama i pružateljima usluga,\n        omogućavajući vam da uživate u vrhunskom iskustvu bez nepotrebnog\n        stresa ili čekanja.\n\n<br>\n\n**Šta nas čini posebnima?**\n\n<br>\n\n*➼ Sve na jednom mjestu: BookMySpot vam pruža širok spektar opcija za\n        rezervaciju, bilo da tražite savršen smještaj za odmor, opuštajući\n        tretman u salonu ljepote, ukusan obrok u restoranu ili stručnu\n        medicinsku uslugu u ordinaciji..*\n\n<br>\n\n*➼ Jednostavno i brzo: Naša platforma je dizajnirana s fokusom na\n        korisničko iskustvo. Brza navigacija i jednostavno sučelje omogućavaju\n        vam da rezervirate svoje mjesto u svega nekoliko klikova.*\n\n<br>\n\n*➼ Recenzije i ocjene: Povratne informacije naših korisnika su nam\n        izuzetno važne. Pružamo vam mogućnost čitanja recenzija drugih\n        korisnika kako biste donijeli informiranu odluku i dijelili vlastita\n        iskustva.*\n\n<br>\n\n*➼ Personalizirane preporuke: Na temelju vaših preferencija i povijesti\n        rezervacija, nudimo vam personalizirane preporuke kako biste uvijek\n        pronašli ono što vam najbolje odgovara.*\n\n</center>"
            };

            await _dbContext.SadrzajiONama.AddAsync(oNamaSadrzaj);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
