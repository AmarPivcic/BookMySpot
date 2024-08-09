using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using BookMySpotAPI.Modul.ViewModels;
namespace BookMySpotAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KorisnikController :ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public KorisnikController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task <ActionResult> Get(int id)
        {
            return Ok(await _dbContext.Korisnici.FirstOrDefaultAsync(x=> x.osobaID == id));
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
