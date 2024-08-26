using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using BookMySpotAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMySpotAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsluzniObjektController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration configuration;

        public UsluzniObjektController(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<UsluzniObjekt>> Get(int usluzniObjektID, int? korisnikID)
        {
            var usluzniObjekt = await _dbContext.UsluzniObjekti.FindAsync(usluzniObjektID);

            if (usluzniObjekt == null)
                return NotFound("Uslužni objekt nije pronađen!");

            var favoriti = new List<int>();

            if(korisnikID != null)
            {
                favoriti = await _dbContext.Favoriti.Where(f => f.KorisnickiNalogId == korisnikID)
                    .Select(f => f.usluzniObjektID)
                    .ToListAsync();
            }
            

            var returnUsluzniObjekt = await _dbContext.UsluzniObjekti.Where(u => u.usluzniObjektID == usluzniObjekt.usluzniObjektID).Include(u => u.grad).Include(u => u.kategorija).Select(u => new UsluzniObjekt
            {
                usluzniObjektID = u.usluzniObjektID,
                nazivObjekta = u.nazivObjekta,
                adresa = u.adresa,
                telefon = u.telefon,
                radnoVrijemePocetak = u.radnoVrijemePocetak,
                radnoVrijemeKraj = u.radnoVrijemeKraj,
                slika = u.slika,
                kategorijaID = u.kategorijaID,
                kategorija = u.kategorija,
                gradID = u.gradID,
                grad = u.grad,
                prosjecnaOcjena = _dbContext.Recenzije.Where(r => r.usluzniObjektID == u.usluzniObjektID).Average(r => (double?)r.recenzijaOcjena),
                isSmjestaj = u.isSmjestaj,
                latitude = u.latitude,
                longitude = u.longitude,
                isFavorit = korisnikID!=null && favoriti.Contains(u.usluzniObjektID)
            }).FirstOrDefaultAsync();

            return Ok(returnUsluzniObjekt);
        }


        [HttpGet]
        public async Task<ActionResult<List<UsluzniObjekt>>> GetByKategorija(int kategorijaID)
        {
            var kategorija = await _dbContext.Kategorije.FindAsync(kategorijaID);
            if (kategorija == null)
                return NotFound("Kategorija nije pronađena!");


            var listaObjekata = await _dbContext.UsluzniObjekti.Where(u => u.kategorijaID == kategorija.kategorijaID).Include(u => u.grad).Include(u => u.kategorija).Select(u => new UsluzniObjekt
            {
                usluzniObjektID = u.usluzniObjektID,
                nazivObjekta = u.nazivObjekta,
                adresa = u.adresa,
                telefon = u.telefon,
                radnoVrijemePocetak = u.radnoVrijemePocetak,
                radnoVrijemeKraj = u.radnoVrijemeKraj,
                slika = u.slika,
                kategorijaID = u.kategorijaID,
                kategorija = u.kategorija,
                gradID = u.gradID,
                grad = u.grad,
                prosjecnaOcjena = _dbContext.Recenzije.Where(r => r.usluzniObjektID == u.usluzniObjektID).Average(r => (double?)r.recenzijaOcjena),
                isSmjestaj = u.isSmjestaj,
                latitude = u.latitude,
                longitude = u.longitude,
            }).ToListAsync();

            return Ok(listaObjekata);
        }

        [HttpGet]
        public async Task<ActionResult<List<UsluzniObjekt>>> GetByProsjecnaOcjena(int kategorijaID)
        {
            var kategorija = await _dbContext.Kategorije.FindAsync(kategorijaID);
            if (kategorija == null)
                return NotFound("Kategorija nije pronađena!");

            var listaObjekata = await _dbContext.UsluzniObjekti.Where(u => u.kategorijaID == kategorija.kategorijaID).Include(u => u.grad).Include(u => u.kategorija).Select(u => new UsluzniObjekt
            {
                usluzniObjektID = u.usluzniObjektID,
                nazivObjekta = u.nazivObjekta,
                adresa = u.adresa,
                telefon = u.telefon,
                radnoVrijemePocetak = u.radnoVrijemePocetak,
                radnoVrijemeKraj = u.radnoVrijemeKraj,
                slika = u.slika,
                kategorijaID = u.kategorijaID,
                kategorija = u.kategorija,
                gradID = u.gradID,
                grad = u.grad,
                prosjecnaOcjena = _dbContext.Recenzije.Where(r => r.usluzniObjektID == u.usluzniObjektID).Average(r => (double?)r.recenzijaOcjena),
                isSmjestaj = u.isSmjestaj,
                latitude = u.latitude,
                longitude = u.longitude,
            }).OrderBy(o => o.prosjecnaOcjena).ToListAsync();

            return Ok(listaObjekata);
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<List<UsluzniObjekt>>> EditKoordinateObjekta([FromRoute] int id, [FromBody] EditKoordinateObjektaVM request)
        {
            var izBaze = await _dbContext.UsluzniObjekti.FirstOrDefaultAsync(u => u.usluzniObjektID == id);

            if(izBaze == null)
            {
                return NotFound();
            }

            izBaze.latitude = request.latitude;
            izBaze.longitude = request.longitude;

            await _dbContext.SaveChangesAsync();

            return Ok(izBaze);  
        }
    }
}
