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
                Naziv = "Smještaj",
                Slika = "https://localhost:7058/Slike/room.jpg"
            };

            await _dbContext.Kategorije.AddAsync(smjestaj);
            await _dbContext.SaveChangesAsync();


            var saloniLjepote = new Kategorija
            {
                Naziv = "Saloni Ljepote",
                Slika = "https://localhost:7058/Slike/beauty.jpg"
            };

            await _dbContext.Kategorije.AddAsync(saloniLjepote);
            await _dbContext.SaveChangesAsync();


            var ugostiteljstvo = new Kategorija
            {
                Naziv = "Ugostiteljstvo",
                Slika = "https://localhost:7058/Slike/restoran.jpg"
            };

            await _dbContext.Kategorije.AddAsync(ugostiteljstvo);
            await _dbContext.SaveChangesAsync();


            var ordinacije = new Kategorija
            {
                Naziv = "Ordinacije",
                Slika = "https://localhost:7058/Slike/ordinacija.jpg"
            };

            await _dbContext.Kategorije.AddAsync(ordinacije);
            await _dbContext.SaveChangesAsync();


            var vozila = new Kategorija
            {
                Naziv = "Vozila",
                Slika = "https://localhost:7058/Slike/vozila.jpg"
            };

            await _dbContext.Kategorije.AddAsync(vozila);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
