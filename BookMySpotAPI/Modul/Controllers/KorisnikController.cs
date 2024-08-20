using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using BookMySpotAPI.Modul.ViewModels;
using BookMySpotAPI.Helper;
using Microsoft.AspNetCore.Identity;
using static BookMySpotAPI.Autentifikacija.Helper.MyAuthTokenExtension;
using BookMySpotAPI.Autentifikacija.Controllers;
using BookMySpotAPI.Autentifikacija.Models;
namespace BookMySpotAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KorisnikController :ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PasswordHasher _passwordHasher;

        public KorisnikController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _passwordHasher = new PasswordHasher();
        }

        [HttpGet]
        public async Task <ActionResult> Get(int id)
        {
            var korisnikIzBaze = await _dbContext.KorisnickiNalog.FirstOrDefaultAsync(x => x.osobaID == id);

            if(korisnikIzBaze != null)
            {
                var korisnikResponse = new KorisnikInformacijeResponseVM
                {
                    korisnickoIme = korisnikIzBaze.korisnickoIme,
                    email = korisnikIzBaze.email,
                    ime = korisnikIzBaze.ime,
                    prezime = korisnikIzBaze.prezime,
                    slika = korisnikIzBaze.slika,
                    telefon = korisnikIzBaze.telefon
                };

                return Ok(korisnikResponse);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> EditKorisnickiRacun([FromRoute] int id, [FromForm] KorisnikInformacijeRequestVM request)
        {
            var korisnikIzBaze = await _dbContext.KorisnickiNalog.FirstOrDefaultAsync(x => x.osobaID == id);

            if (korisnikIzBaze == null)
            {
                return NotFound();
            }

            if(request.slika != null)
            {
                var slike = new Slike(_webHostEnvironment);
                var slikaKorisnika = slike.dodajSliku(request.slika);
                korisnikIzBaze.slika = slikaKorisnika;
            }

            korisnikIzBaze.ime = request.ime;
            korisnikIzBaze.prezime = request.prezime;
            korisnikIzBaze.email = request.email;
            korisnikIzBaze.telefon = request.telefon;
            korisnikIzBaze.korisnickoIme = request.korisnickoIme;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> NoviUserName([FromRoute] int id, [FromQuery] string newUserName)
        {
            var korisnikIzBaze = await _dbContext.KorisnickiNalog.FirstOrDefaultAsync(x => x.osobaID == id);

            if (korisnikIzBaze == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(newUserName))
            {
                var korisnikSaIstimKorisnickimImenom = await _dbContext.KorisnickiNalog
                    .FirstOrDefaultAsync(x => x.korisnickoIme == newUserName && x.osobaID != id);

                if (korisnikSaIstimKorisnickimImenom != null)
                {
                    return Conflict("Korisničko ime već postoji.");
                }

                korisnikIzBaze.korisnickoIme = newUserName;
                await _dbContext.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> EditLozinkuZaKorisnickiNalog([FromBody] StaraNovaLozinkaRequestVM zahtjev)
        {
            var logiranaOsoba = await _dbContext.KorisnickiNalog
                .FirstOrDefaultAsync(k => k.korisnickoIme != null && k.korisnickoIme == zahtjev.korisnickoIme);

            if (logiranaOsoba == null)
            {
                return NotFound("Korisnik nije pronađen.");
            }

            if (_passwordHasher.VerifyHashedPassword(logiranaOsoba, logiranaOsoba.lozinka, zahtjev.staraLozinka) != PasswordVerificationResult.Success)
            {
                return BadRequest("Stara lozinka nije ispravna.");
            }

            var hashedPassword = _passwordHasher.HashPassword(logiranaOsoba, zahtjev.novaLozinka);
            logiranaOsoba.lozinka = hashedPassword;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> ObrisiKorisnickiRacun([FromBody] ObrisiLozinkuRequestVM zahtjev)
        {
            var logiranaOsoba = await _dbContext.KorisnickiNalog
                .FirstOrDefaultAsync(k => k.korisnickoIme != null && k.korisnickoIme == zahtjev.korisnickoIme);

            if (logiranaOsoba == null)
            {
                return NotFound("Korisnik nije pronađen.");
            }

            if (_passwordHasher.VerifyHashedPassword(logiranaOsoba, logiranaOsoba.lozinka, zahtjev.lozinka) != PasswordVerificationResult.Success)
            {
                return BadRequest("Lozinka nije ispravna.");
            }

            logiranaOsoba.obrisan = true;
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task <ActionResult> PromijeniIme([FromBody] KorisnickiNalogEditVM x)
        {
            KorisnickiNalog osoba = await _dbContext.KorisnickiNalog.FirstOrDefaultAsync(k => k.osobaID == x.OsobaID);

            if(osoba == null)
            {
                return BadRequest("Pogrešan ID");
            }
            else
            {
                osoba.ime = x.novoIme;
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpPost]
        public async Task <ActionResult> PromijenPrezime([FromBody] KorisnickiNalogEditVM x)
        {
            KorisnickiNalog osoba = await _dbContext.KorisnickiNalog.FirstOrDefaultAsync(k => k.osobaID == x.OsobaID);

            if (osoba == null)
            {
                return BadRequest("Pogrešan ID");
            }
            else
            {
                osoba.prezime = x.novoPrezime;
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpPost]
        public async Task <ActionResult> PromijeniEmail([FromBody] KorisnickiNalogEditVM x)
        {
            KorisnickiNalog osoba = await _dbContext.KorisnickiNalog.FirstOrDefaultAsync(k => k.osobaID == x.OsobaID);

            if (osoba == null)
            {
                return BadRequest("Pogrešan ID");
            }
            else
            {
                osoba.email = x.noviEmail;
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpPost]
        public async Task<ActionResult> PromijeniTelefon([FromBody] KorisnickiNalogEditVM x)
        {
            KorisnickiNalog osoba =await _dbContext.KorisnickiNalog.FirstOrDefaultAsync(k => k.osobaID == x.OsobaID);

            if (osoba == null)
            {
                return BadRequest("Pogrešan ID");
            }
            else
            {
                osoba.telefon = x.noviTelefon;
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpPost]
        public async  Task<ActionResult> PromijeniKorisnickoIme([FromBody] KorisnickiNalogEditVM x)
        {
            KorisnickiNalog osoba = await _dbContext.KorisnickiNalog.FirstOrDefaultAsync(k => k.osobaID == x.OsobaID);

            if (osoba == null)
            {
                return BadRequest("Pogrešan ID");
            }
            else
            {
                osoba.korisnickoIme = x.novoKorisnickoIme;
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
