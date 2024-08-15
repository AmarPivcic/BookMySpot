using BookMySpotAPI.Data;
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
        public ActionResult GetByObjektID(int id)
        {
            var listaUsluga = _dbContext.Usluge.Where(x => x.usluzniObjektID == id).Include(u => u.usluzniObjekt).ToList();
            return Ok(listaUsluga);
        }
    }
}
