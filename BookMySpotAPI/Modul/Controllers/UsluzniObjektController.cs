using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMySpotAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsluzniObjektController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public UsluzniObjektController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Get (int id)
        {
            var usluzniObjekt = await _dbContext.UsluzniObjekti.Where(u => u.usluzniObjektID == id).Include(u => u.grad).Include(u => u.kategorija).Select(u => new UsluzniObjekt
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
                prosjecnaOcjena = _dbContext.Recenzije.Where(r => r.usluzniObjektID == u.usluzniObjektID).Average(r => (double?)r.recenzijaOcjena)
            }).FirstOrDefaultAsync();

            return Ok(usluzniObjekt);
        }

        [HttpGet]
        public async Task<ActionResult<List<UsluzniObjekt>>> GetByKategorijaID(int id)
        {
            var listaObjekata = await _dbContext.UsluzniObjekti.Where(u => u.kategorijaID == id).Include(u => u.grad).Include(u => u.kategorija).Select(u => new UsluzniObjekt
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
                prosjecnaOcjena = _dbContext.Recenzije.Where(r => r.usluzniObjektID == u.usluzniObjektID).Average(r => (double?)r.recenzijaOcjena)
            }).ToListAsync();

            return Ok(listaObjekata);
        }
    }
}
