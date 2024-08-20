using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMySpotAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UslugaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public UslugaController(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usluga>>> GetByObjektID(int id)
        {
            var usluzniObjekt = await _dbContext.UsluzniObjekti.FindAsync(id);
            if (usluzniObjekt == null)
                return NotFound("Uslužni objekt nije pronađen!");

            var listaUsluga = _dbContext.Usluge.Where(x => x.usluzniObjektID == usluzniObjekt.usluzniObjektID).Include(u => u.usluzniObjekt).ToList();
            return Ok(listaUsluga);
        }
    }
}
