using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using BookMySpotAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMySpotAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RecenzijaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public RecenzijaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Recenzija>>> GetByUsluzniObjektID(int id)
        {
            var usluzniObjekt = await _dbContext.UsluzniObjekti.FindAsync(id);

            if (usluzniObjekt == null)
                return NotFound("Uslužni objekt nije pronađen!");

            var listaRecenzija = await _dbContext.Recenzije.Where(r => r.usluzniObjektID == usluzniObjekt.usluzniObjektID).Include(r => r.korisnickiNalog).ToListAsync();
            return Ok(listaRecenzija);
        }

        [HttpPost]
        public async Task <ActionResult> Add([FromBody] RecenzijaAddVM x)
        {
            var novaRecenzija = new Recenzija
            {
                recenzijaOcjena = x.recenzijaOcjena,
                recenzijaTekst = x.recenzijaTekst,
                KorisnickiNalogId = x.osobaID,
                usluzniObjektID = x.usluzniObjektID
            };

            await _dbContext.Recenzije.AddAsync(novaRecenzija);
            await _dbContext.SaveChangesAsync();

            return Ok(novaRecenzija);
        }
    }
}
