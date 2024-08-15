using BookMySpotAPI.Data;
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
            var usluzniObjekt = await _dbContext.UsluzniObjekti.FirstOrDefaultAsync(x => x.usluzniObjektID == id);
            return Ok(usluzniObjekt);
        }

        [HttpGet]
        public ActionResult GetByKategorijaID(int id)
        {
            var listaObjekata =  _dbContext.UsluzniObjekti.Where(x => x.kategorijaID == id).Include(u => u.grad).Include(u => u.kategorija).ToList();
            return Ok(listaObjekata);
        }
    }
}
