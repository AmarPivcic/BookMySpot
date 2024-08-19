using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using BookMySpotAPI.Modul.ViewModels;
using BookMySpotAPI.Helper;
namespace BookMySpotAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KorisnikController :ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public KorisnikController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
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
