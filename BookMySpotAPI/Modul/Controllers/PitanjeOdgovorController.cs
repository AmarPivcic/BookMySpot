using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using BookMySpotAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMySpotAPI.Modul.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PitanjeOdgovorController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public PitanjeOdgovorController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> PostaviPitanje([FromBody] PitanjeOdgovorRequestVM request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var zaBazu = new PitanjeOdgovor
            {
                KorisnickiNalogId = request.KorisnickiNalogId,
                DatumKreiranja = DateTime.Now,
                Pitanje = request.Pitanje
            };

            await dbContext.PitanjaOdgovori.AddAsync(zaBazu);
            await dbContext.SaveChangesAsync();

            return Ok(zaBazu);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> PostaviOdgovor(int id, [FromBody] OdgovorRequestVM noviOdgovor)
        {
            if (string.IsNullOrEmpty(noviOdgovor.Odgovor))
            {
                return BadRequest("Odgovor ne može biti prazan!");
            }

            var pitanjeOdgovor = await dbContext.PitanjaOdgovori.FindAsync(id);

            if (pitanjeOdgovor == null)
            {
                return NotFound($"Pitanje sa Id: {id} nije pronađeno.");
            }

            pitanjeOdgovor.Odgovor = noviOdgovor.Odgovor;

            await dbContext.SaveChangesAsync();

            return Ok(pitanjeOdgovor);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdatePitanjeOdgovorById([FromRoute] int id, [FromBody] UpdatePitanjeOdgovorRequest request)
        {
            var izBaze = await dbContext.PitanjaOdgovori.FirstOrDefaultAsync(x => x.Id == id);

            if (izBaze == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(request.Pitanje))
            {
                izBaze.Pitanje = request.Pitanje;
            }

            if (!string.IsNullOrEmpty(request.Odgovor))
            {
                izBaze.Odgovor = request.Odgovor;
            }

            await dbContext.SaveChangesAsync();

            return Ok(izBaze);
        }

        [HttpGet]
        public async Task<IActionResult> VratiSvaPitanjaAdministrator([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {

            pageNumber = (pageNumber < 1) ? 1 : pageNumber;
            pageSize = (pageSize < 1 || pageSize > 100) ? 5 : pageSize;

            var totalPitanja = await dbContext.PitanjaOdgovori.CountAsync();

            var pitanjaOdgovori = await dbContext.PitanjaOdgovori.Include(p => p.korisnickiNalog).Select(p => new PitanjaOdgovoriResponseVM
            {
                Id = p.Id,
                Pitanje = p.Pitanje,
                Odgovor = p.Odgovor,
                DatumKreiranja = p.DatumKreiranja,
                osobaID = p.korisnickiNalog.osobaID,
                email = p.korisnickiNalog.email,
                ime = p.korisnickiNalog.ime,
                korisnickoIme = p.korisnickiNalog.korisnickoIme,
                prezime = p.korisnickiNalog.prezime,
                telefon = p.korisnickiNalog.telefon
            }).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalPitanja / pageSize);

            // Kreiranje odgovora
            var response = new
            {
                Pitanja = pitanjaOdgovori,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetPitanjeById([FromRoute] int id)
        {
            var izBaze = await dbContext.PitanjaOdgovori.FirstOrDefaultAsync(x => x.Id == id);

            if (izBaze == null)
            {
                return NotFound();
            }

            var zaKorisnika = new PitanjaOdgovoriResponseAdminEdit
            {
                Id = izBaze.Id,
                Pitanje = izBaze.Pitanje,
                Odgovor = izBaze.Odgovor
            };

            return Ok(zaKorisnika);
        }

        //[HttpGet("UserView")]
        //public async Task<IActionResult> VratiSvaPitanjaKorisnik()
        //{
        //    var pitanjaOdgovori = await dbContext.PitanjaOdgovori.Include(p => p.korisnickiNalog).Select(p => new PitanjaOdgovoriResponseUserVM
        //    {
        //        Id = p.Id,
        //        Pitanje = p.Pitanje,
        //        Odgovor = p.Odgovor,
        //        DatumKreiranja = p.DatumKreiranja,
        //    }).ToListAsync();

        //    if (pitanjaOdgovori == null || pitanjaOdgovori.Count == 0)
        //    {
        //        return Ok("Nema dostupnih podataka u bazi!");
        //    }

        //    return Ok(pitanjaOdgovori);
        //}

        [HttpGet]
        public async Task<IActionResult> VratiNeodgovorenaPitanja()
        {
            var pitanjaOdgovori = await dbContext.PitanjaOdgovori.Include(p => p.korisnickiNalog).Where(p => p.Odgovor == null).Select(p => new PitanjaOdgovoriResponseUserVM
            {
                Id = p.Id,
                Pitanje = p.Pitanje,
                Odgovor = p.Odgovor,
                DatumKreiranja = p.DatumKreiranja,
            }).ToListAsync();

            return Ok(pitanjaOdgovori);
        }

        [HttpGet]
        public async Task<IActionResult> VratiBrojNeodgovorenihPitanja()
        {
            var broj = dbContext.PitanjaOdgovori.Where(p => p.Odgovor == null).Count();
            return Ok(broj);
        }

        [HttpGet]
        public async Task<IActionResult> VratiOdgovorenaPitanja(string? filter = "")
        {
            var filterLower = filter?.Trim().ToLower() ?? string.Empty;
            var pitanjaOdgovori = await dbContext.PitanjaOdgovori
                .Include(p => p.korisnickiNalog)
                .Where(p => p.Odgovor != null)
                .Where(p => p.Pitanje.ToLower()
                .Contains(filterLower))
                .Select(p => new PitanjaOdgovoriResponseUserVM
                {
                    Id = p.Id,
                    Pitanje = p.Pitanje,
                    Odgovor = p.Odgovor,
                    DatumKreiranja = p.DatumKreiranja,
                }).ToListAsync();

            return Ok(pitanjaOdgovori);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> ObrisiPitanjeOdgovor([FromRoute] int id)
        {
            var zaObrisati = await dbContext.PitanjaOdgovori.FirstOrDefaultAsync(p => p.Id == id);

            if (zaObrisati == null)
            {
                return NotFound("Unijeli ste nepostojeci Id!");
            }

            dbContext.PitanjaOdgovori.Remove(zaObrisati);
            await dbContext.SaveChangesAsync();

            return Ok(zaObrisati);
        }
    }
}
