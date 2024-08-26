using Azure.Core;
using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using BookMySpotAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMySpotAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FavoritController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public FavoritController(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult> Add ([FromBody] FavoritAddVM x)
        {
            var favorit = new Favorit
            {
                KorisnickiNalogId = x.korisnickiNalogId,
                usluzniObjektID = x.usluzniObjektID,
            };

            await _dbContext.Favoriti.AddAsync(favorit);
            await _dbContext.SaveChangesAsync();

            return Ok(favorit);
        }

        [HttpDelete]
        public async Task<ActionResult> Remove([FromBody] FavoritRemoveVM x)
        {
            var favorit = await _dbContext.Favoriti.FirstOrDefaultAsync(f => f.usluzniObjektID == x.usluzniObjektID && f.KorisnickiNalogId == x.korisnickiNalogId);

            if (favorit == null)
                return NotFound();

            _dbContext.Remove(favorit);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<UsluzniObjekt>>> GetListaFavorita(int korisnikID, int kategorijaID)
        {
            var korisnik = _dbContext.Korisnici.Find(korisnikID);

            if (korisnik == null)
            {
                return NotFound();
            }

            var kategorija = _dbContext.Kategorije.Find(kategorijaID);

            if(kategorija == null)
            {
                return NotFound();
            }

            var favoriti = await _dbContext.Favoriti.Where(f => f.KorisnickiNalogId == korisnikID)
                .Select(f => f.usluzniObjektID)
                .ToListAsync();

            var listaFavorita = await _dbContext.Favoriti.Where(f => f.KorisnickiNalogId == korisnik.osobaID && f.usluzniObjekt.kategorijaID == kategorija.kategorijaID).Include(f => f.usluzniObjekt).Include(f => f.usluzniObjekt.grad).Select(u => new UsluzniObjekt
            {
                usluzniObjektID = u.usluzniObjektID,
                nazivObjekta = u.usluzniObjekt.nazivObjekta,
                adresa = u.usluzniObjekt.adresa,
                telefon = u.usluzniObjekt.telefon,
                radnoVrijemePocetak = u.usluzniObjekt.radnoVrijemePocetak,
                radnoVrijemeKraj = u.usluzniObjekt.radnoVrijemeKraj,
                slika = u.usluzniObjekt.slika,
                kategorijaID = u.usluzniObjekt.kategorijaID,
                kategorija = u.usluzniObjekt.kategorija,
                gradID = u.usluzniObjekt.gradID,
                grad = u.usluzniObjekt.grad,
                prosjecnaOcjena = _dbContext.Recenzije.Where(r => r.usluzniObjektID == u.usluzniObjektID).Average(r => (double?)r.recenzijaOcjena),
                isSmjestaj = u.usluzniObjekt.isSmjestaj,
                latitude = u.usluzniObjekt.latitude,
                longitude = u.usluzniObjekt.longitude,
                isFavorit = favoriti.Contains(u.usluzniObjektID)
            }).ToListAsync();

            return Ok(listaFavorita);
        }
    }
}
